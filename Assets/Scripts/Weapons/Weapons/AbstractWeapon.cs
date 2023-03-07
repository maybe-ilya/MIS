using mis.Core;
using UnityEngine;

namespace mis.Weapons
{
    internal abstract class AbstractWeapon : AbstractGameEntityComponent,
        IWeapon,
        IWeaponModuleOwner
    {
        [SerializeField]
        private GameId _consumedAmmo;

        public GameId AmmoType => _consumedAmmo;

        public IWeaponOwner WeaponOwner { get; private set; }

        public void OnEquip(IWeaponOwner owner)
        {
            WeaponOwner = owner;
            OnEquipInternal();
        }

        public void OnUnequip()
        {
            WeaponOwner = null;
            OnUnequipInternal();
        }

        protected virtual void OnEquipInternal() { }

        protected virtual void OnUnequipInternal() { }

        public virtual void StartFire() { }

        public virtual void StopFire() { }

        public virtual void StartAltFire() { }

        public virtual void StopAltFire() { }
    }
}