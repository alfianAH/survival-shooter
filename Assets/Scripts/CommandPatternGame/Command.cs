namespace CommandPatternGame
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }

    public class MoveCommand : Command
    {
        private PlayerMovement playerMovement;
        private float horizontal, vertical;
    
        public MoveCommand(PlayerMovement playerMovement, float horizontal, float vertical)
        {
            this.playerMovement = playerMovement;
            this.horizontal = horizontal;
            this.vertical = vertical;
        }
    
        public override void Execute()
        {
            playerMovement.Move(horizontal, vertical);
            playerMovement.WalkingAnimation(horizontal, vertical);
        }

        public override void UnExecute()
        {
            playerMovement.Move(-horizontal, -vertical);
            playerMovement.WalkingAnimation(horizontal, vertical);
        }
    }

    public class ShootingCommand : Command
    {
        private PlayerShooting playerShooting;

        public ShootingCommand(PlayerShooting playerShooting)
        {
            this.playerShooting = playerShooting;
        }
    
        public override void Execute()
        {
            playerShooting.Shoot();
        }

        public override void UnExecute() { }
    }
}