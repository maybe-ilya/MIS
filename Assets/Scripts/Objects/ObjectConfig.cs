using UnityEngine;
using mis.Core;

namespace mis.Objects
{
    [CreateAssetMenu(fileName = "ObjectConfig", menuName = "mis Assets/ObjectConfig")]
    public sealed class ObjectConfig : AbstractConfig, IObjectConfig
    {
        [SerializeField]
        [CheckObject]
        private GameObject _prefab;
        [SerializeField]
        private int _minPooledObjects;
        [SerializeField]
        private int _maxPooledObjects;
        [SerializeField]
        private bool _needInitResources;
        [SerializeField]
        private Resource[] _resources;
        [SerializeField]
        [OneLine]
        private ResourceLimit[] _resourceLimits;

        public GameObject Prefab => _prefab;
        public int MinPooledObjects => _minPooledObjects;
        public int MaxPooledObjects => _maxPooledObjects;
        public bool NeedInitializeResources => _needInitResources;
        public Resource[] Resources => _resources;
        public ResourceLimit[] ResourceLimits => _resourceLimits;
    }
}