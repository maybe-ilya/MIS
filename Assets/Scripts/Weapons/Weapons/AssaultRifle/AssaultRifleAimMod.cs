using UnityEngine;

namespace mis.Weapons
{
    internal class AssaultRifleAimMod : AbstractWeapon
    {
        [SerializeField]
        private ConstantFireModule _mainModule;
        [SerializeField]
        private ConstantFireModule _aimModule;

        private bool _isAimModuleSelected;

        private ConstantFireModule ActiveModule =>
            _isAimModuleSelected ? _aimModule : _mainModule;

        protected override void OnEquipInternal()
        {
            _mainModule.SetOwner(this);
            _aimModule.SetOwner(this);
        }

        public override void StartFire()
        {
            ActiveModule.StartShooting();
        }

        public override void StopFire()
        {
            ActiveModule.StopShooting();
        }

        public override void StartAltFire()
        {
            _isAimModuleSelected = true;
        }

        public override void StopAltFire()
        {
            _isAimModuleSelected = false;
        }
    }
}