using mis.Core;
using UnityEngine;
using URandom = UnityEngine.Random;

namespace mis.Audio
{
    [CreateAssetMenu(fileName = "RandomSoundCue", menuName = "mis Assets/Audio/RandomSoundCue")]
    internal sealed class RandomSoundCue : BaseSoundCue
    {
        protected override SoundCueClipSetting PickClip(AbstractSoundCuePlayer player)
        {
            var index = URandom.Range(0, _clips.Length);
            return _clips[index];
        }
    }
}