namespace mis.Core
{
    public interface IPlayer
    {
        int Index { get; }
        IPlayerController Controller { get; }
        void Control(IPlayerController playerController);
    }
}
