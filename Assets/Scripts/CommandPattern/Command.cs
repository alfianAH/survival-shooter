using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public abstract class Command
    {
        [Tooltip("How far the box should move when we press a button")]
        protected float moveDistance = 1f;
    
        /// <summary>
        /// Move and maybe save command
        /// </summary>
        /// <param name="boxTransform"></param>
        /// <param name="command"></param>
        public abstract void Execute(Transform boxTransform, Command command);
    
        /// <summary>
        /// Undo an old command
        /// </summary>
        /// <param name="boxTransform"></param>
        public virtual void Undo(Transform boxTransform){}
    
        /// <summary>
        /// Move the box
        /// </summary>
        /// <param name="boxTransform"></param>
        public virtual void Move(Transform boxTransform){}
    }

    public class MoveForward : Command
    {
        public override void Execute(Transform boxTransform, Command command)
        {
            Move(boxTransform); // Move the box
            InputHandler.OldCommands.Add(command); // Save the command
        }

        public override void Undo(Transform boxTransform)
        {
            boxTransform.Translate(-boxTransform.forward * moveDistance);
        }

        public override void Move(Transform boxTransform)
        {
            boxTransform.Translate(boxTransform.forward * moveDistance);
        }
    }

    public class MoveReserve : Command
    {
        public override void Execute(Transform boxTransform, Command command)
        {
            Move(boxTransform); // Move the box
            InputHandler.OldCommands.Add(command); // Save the command
        }

        public override void Undo(Transform boxTransform)
        {
            boxTransform.Translate(boxTransform.forward * moveDistance);
        }

        public override void Move(Transform boxTransform)
        {
            boxTransform.Translate(-boxTransform.forward * moveDistance);
        }
    }

    public class MoveLeft : Command
    {
        public override void Execute(Transform boxTransform, Command command)
        {
            Move(boxTransform); // Move the box
            InputHandler.OldCommands.Add(command); // Save the command
        }

        public override void Undo(Transform boxTransform)
        {
            boxTransform.Translate(boxTransform.right * moveDistance);
        }

        public override void Move(Transform boxTransform)
        {
            boxTransform.Translate(-boxTransform.right * moveDistance);
        }
    }

    public class MoveRight : Command
    {
        public override void Execute(Transform boxTransform, Command command)
        {
            Move(boxTransform); // Move the box
            InputHandler.OldCommands.Add(command); // Save the command
        }

        public override void Undo(Transform boxTransform)
        {
            boxTransform.Translate(-boxTransform.right * moveDistance);
        }

        public override void Move(Transform boxTransform)
        {
            boxTransform.Translate(boxTransform.right * moveDistance);
        }
    }

    public class DoNothing : Command
    {
        public override void Execute(Transform boxTransform, Command command)
        {
            // Do nothing
        }
    }

    public class UndoCommand : Command
    {
        public override void Execute(Transform boxTransform, Command command)
        {
            List<Command> oldCommands = InputHandler.OldCommands;

            if (oldCommands.Count > 0)
            {
                Command latestCommand = oldCommands[oldCommands.Count - 1];
            
                // Move the box with this command
                latestCommand.Undo(boxTransform);
            
                // Remove the command from the list
                oldCommands.RemoveAt(oldCommands.Count - 1);
            }
        }
    }

    public class ReplayCommand : Command
    {
        public override void Execute(Transform boxTransform, Command command)
        {
            InputHandler.ShouldStartReplay = true;
        }
    }
}