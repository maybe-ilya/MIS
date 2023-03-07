using mis.Core;

namespace mis.Weapons
{
    internal interface IWeaponModuleOwner
    {
        GameId AmmoType { get; }
        IWeaponOwner WeaponOwner { get; }
    }
}