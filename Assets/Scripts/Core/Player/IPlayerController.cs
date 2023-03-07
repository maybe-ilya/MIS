namespace mis.Core
{
    public interface IPlayerController
    {
        int PlayerIndex { get; }
        IPlayerPawn Pawn { get; }
        void Setup(IPlayer player);
        void ControlPawn(IPlayerPawn pawn);
        void ReleasePawn();
        void SetPawnInputMode();
        void SetUIInputMode();
    }
}
