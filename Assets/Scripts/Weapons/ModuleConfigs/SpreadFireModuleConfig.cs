using mis.Core;
using UnityEngine;

namespace mis.Weapons
{
    [CreateAssetMenu(fileName = "SpreadFireModuleConfig", menuName = "mis Assets/Weapons/Configs/SpreadFireModuleConfig")]
    internal class SpreadFireModuleConfig : AbstractWeaponModuleConfig
    {
        [SerializeField]
        private uint _shotsCount;

        [SerializeField]
        private float _shotCooldown;

        [SerializeField]
        private GameId _projectileId;

        public uint ShotsCount => _shotsCount;

        public float ShotCooldown => _shotCooldown;

        public GameId ProjectileId => _projectileId;
    }
}