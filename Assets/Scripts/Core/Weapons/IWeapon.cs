namespace mis.Core
{
    public interface IWeapon : IGameEntityComponent
    {
        GameId AmmoType { get; }
        void OnEquip(IWeaponOwner newOwner);
        void OnUnequip();
        void StartFire();
        void StopFire();
        void StartAltFire();
        void StopAltFire();
    }
}