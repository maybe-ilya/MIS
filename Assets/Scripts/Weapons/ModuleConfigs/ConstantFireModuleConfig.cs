using mis.Core;
using UnityEngine;

namespace mis.Weapons
{
    [CreateAssetMenu(fileName = "ConstantFireModuleConfig", menuName = "mis Assets/Weapons/Configs/ConstantFireModuleConfig")]
    internal class ConstantFireModuleConfig : AbstractWeaponModuleConfig
    {
        [SerializeField]
        private float _shotCooldown;

        [SerializeField]
        private GameId _projectileId;

        public float ShotCooldown => _shotCooldown;

        public GameId ProjectileId => _projectileId;
    }
}