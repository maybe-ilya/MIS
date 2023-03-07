using mis.Core;
using UnityEngine;

namespace mis.Damage
{
    [CreateAssetMenu(fileName = "DamageConfig", menuName = "mis Assets/DamageConfig")]
    internal sealed class DamageConfig : AbstractConfig, IDamageConfig
    {
        [SerializeField]
        private float _armorDefenceRatio;

        [SerializeField]
        private float _armorDefencePower;

        public float ArmorDefenceRatio => _armorDefenceRatio;
        
        public float ArmorDefencePower => _armorDefencePower;
    }
}