﻿using System.Collections.Generic;
using UnityEngine;

namespace CommandPatternGame
{
    public class InputHandler : MonoBehaviour
    {
        public PlayerShooting playerShooting;
        
        private PlayerHealth playerHealth;
        private PlayerMovement playerMovement;
        
        private readonly Queue<Command> commands = new Queue<Command>();

        private void Awake()
        {
            playerHealth = PlayerHealth.Instance;
            playerMovement = PlayerMovement.Instance;
        }

        private void FixedUpdate()
        {
            Command moveCommand = InputMovementHandling();
            if (moveCommand != null && playerHealth.currentHealth > 0)
            {
                commands.Enqueue(moveCommand);
                moveCommand.Execute();
            }
        }

        private void Update()
        {
            Command shootCommand = InputShootHandling();
            shootCommand?.Execute();
        }
        
        /// <summary>
        /// Keys for movement
        /// </summary>
        /// <returns></returns>
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
