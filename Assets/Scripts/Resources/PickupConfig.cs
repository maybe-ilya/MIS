using mis.Core;
using UnityEngine;

namespace mis.Resources
{
    [CreateAssetMenu(fileName = "PickupConfig", menuName = "mis Assets/PickupConfig")]
    public sealed class PickupConfig : AbstractConfig, IPickupConfig
    {
        [SerializeField]
        private Resource[] _resources;

        public Resource[] Resources => _resources;
    }
}