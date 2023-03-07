using UnityEngine;

namespace mis.Weapons
{
    internal sealed class CombatShotgun : AbstractWeapon
    {
        [SerializeField]
        private SpreadFireModule _mainModule;

        protected override void OnEquipInternal()
        {
            _mainModule.SetOwner(this);
        }

        public override void StartFire()
        {
            _mainModule.StartShooting();
        }

        public override void StopFire()
        {
            _mainModule.StopShooting();
        }
    }
}