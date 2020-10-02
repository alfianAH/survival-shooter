using System.Collections.Generic;
using UnityEngine;

namespace CommandPatternGame
{
    public class InputHandler : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public PlayerShooting playerShooting;
        
        private Queue<Command> commands = new Queue<Command>();
        
        private void FixedUpdate()
        {
            Command moveCommand = InputMovementHandling();
            if (moveCommand != null)
            {
                commands.Enqueue(moveCommand);
                moveCommand.Execute();
            }
        }

        private void Update()
        {
            Command shootCommand = InputShootHandling();
            if (shootCommand != null)
            {
                shootCommand.Execute();
            }
        }

        private Command InputMovementHandling()
        {
            if (Input.GetKey(KeyCode.W))
            {
                return new MoveCommand(playerMovement, 0, 1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                return new MoveCommand(playerMovement, -1, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                return new MoveCommand(playerMovement, 0, -1);
            }

            if (Input.GetKey(KeyCode.D))
            {
                return new MoveCommand(playerMovement, 1, 0);
            }

            if (Input.GetKey(KeyCode.Z))
            {
                return Undo();
            }

            return new MoveCommand(playerMovement, 0, 0);
        }

        private Command Undo()
        {
            // If command Queue is not null, undo
            if (commands.Count > 0)
            {
                Command undoCommand = commands.Dequeue();
                undoCommand.UnExecute();
            }

            return null;
        }

        private Command InputShootHandling()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                return new ShootingCommand(playerShooting);
            }

            return null;
        }
    }
}
