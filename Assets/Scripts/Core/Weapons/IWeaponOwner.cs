namespace mis.Core
{
    public interface IWeaponOwner : IGameEntityComponent
    {
        IViewPoint ViewPoint { get; }
        BaseResourceContainer ResourceContainer { get; }
    }
}