using UnityEngine;

namespace mis.Weapons
{
    internal sealed class AssaultRifle : AbstractWeapon
    {
        [SerializeField]
        private ConstantFireModule _mainModule;

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