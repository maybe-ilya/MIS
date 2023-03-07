using mis.Core;
using UnityEngine;

namespace mis.Drop
{
    [CreateAssetMenu(fileName = "DropDataContainer", menuName = "mis Assets/DropDataContainer")]
    internal sealed class DropDataContainer : ScriptableObject, IDropData
    {
        [SerializeField]
        private Resource[] _resources;

        public Resource[] Resources => _resources;
    }
}