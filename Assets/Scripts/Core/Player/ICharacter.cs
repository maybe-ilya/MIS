namespace mis.Core
{
    public interface ICharacter : IPlayerPawn
    {
        IViewPoint ViewPoint { get; }
    }
}
