using System;
using UnityEngine;

namespace mis.Core
{
    public abstract class BaseWeaponHandler : MonoBehaviour
    {
        public abstract IWeapon EqippedWeapon { get; }
        public abstract GameId EquippedWeaponType { get; }
        public abstract bool IsEquippedAnyWeapon { get; }

        public abstract event Action<IWeapon> OnWeaponChanged;

        public abstract void SetOwner(IWeaponOwner owner);
        public abstract void LoseOwner();
        public abstract void EquipWeapon(GameId weaponId);
        public abstract void EquipLastWeapon();
        public abstract void UnequipCurrentWeapon();
        public abstract void StartFire();
        public abstract void StopFire();
        public abstract void StartAltFire();
        public abstract void StopAltFire();
    }
}