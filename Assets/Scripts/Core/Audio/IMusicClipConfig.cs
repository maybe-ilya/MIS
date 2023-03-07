using UnityEngine;

namespace mis.Core
{
    public interface IMusicClipConfig : IConfig
    {
        AudioClip Clip { get; }
        bool IsLooped { get; }
        float Volume { get; }
    }
}