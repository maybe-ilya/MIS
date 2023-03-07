using UnityEngine;

namespace mis.Core
{
    public interface IUIServiceConfig : IConfig
    {
        GameObject UIEventSystemPrefab { get; }
        GameObject UICanvasPrefab { get; }
    }
}