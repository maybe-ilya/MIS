using mis.Core;
using UnityEngine;

namespace mis.Weapons
{
    internal abstract class AbstractWeaponModuleConfig : ScriptableObject
    {
        [SerializeField]
        private LayerMask _collisionMask;
        [SerializeField]
        private FloatRange _horizontalSpread;
        [SerializeField]
        private FloatRange _verticalSpread;
        [SerializeField]
        private uint _ammoPerShot;
        [SerializeField]
        private DamageData _damageData;

        public LayerMask CollisionMask => _collisionMask;

        public float Distance => _damageData.MaxDistance;

        public FloatRange HorizontalSpread => _horizontalSpread;

        public FloatRange VerticalSpread => _verticalSpread;

        public uint AmmoPerShot => _ammoPerShot;

        public DamageData DamageData => _damageData;
    }
}