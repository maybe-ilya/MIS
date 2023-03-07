using UnityEngine;

namespace mis.Core
{
    public interface IObjectConfig : IConfig
    {
        GameObject Prefab { get; }
        int MinPooledObjects { get; }
        int MaxPooledObjects { get; }

        bool NeedInitializeResources { get; }
        Resource[] Resources { get; }
        ResourceLimit[] ResourceLimits { get; }
    }
}