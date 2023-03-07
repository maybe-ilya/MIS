using mis.Core;
using UnityEngine;

namespace mis.Audio
{
    [CreateAssetMenu(fileName = "MusicClipConfig", menuName = "mis Assets/MusicClipConfig")]
    internal sealed class MusicClipConfig : AbstractConfig, IMusicClipConfig
    {
        [SerializeField]
        [CheckObject]
        private AudioClip _musicClip;

        [SerializeField]
        private bool _isLooped;

        [SerializeField]
        private float _volume;

        public AudioClip Clip => _musicClip;

        public bool IsLooped => _isLooped;

        public float Volume => _volume;
    }
}