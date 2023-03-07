using UnityEngine;

namespace mis.Weapons
{
    internal sealed class CombatShotgunFullAuto : AbstractWeapon
    {
        [SerializeField]
        private SpreadFireModule _mainModule;

        [SerializeField]
        private SpreadFireModule _fullAutoModule;

        private bool _isFullAutoActive;

        private SpreadFireModule ActiveModule =>
            _isFullAutoActive ? _fullAutoModule : _mainModule;

        protected override void OnEquipInternal()
        {
            _mainModule.SetOwner(this);
            _fullAutoModule.SetOwner(this);
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
            _isFullAutoActive = true;
        }

        public override void StopAltFire()
        {
            _isFullAutoActive = false;
        }
    }
}