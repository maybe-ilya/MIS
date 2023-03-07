using mis.Core;

namespace mis.Player
{
    public class Player : IPlayer
    {
        public int Index { get; private set; }
        public IPlayerController Controller { get; private set; }

        public Player(int index)
        {
            Index = index;
        }

        public void Control(IPlayerController playerController)
        {
            Controller = playerController;
            playerController.Setup(this);
        }
    }
}
