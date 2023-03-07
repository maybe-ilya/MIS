using mis.Core;
using UnityEngine;

namespace mis.Audio
{
    public sealed class SequenceCueState : ISoundCueState
    {
        public int NextClipIndex;
    }

    [CreateAssetMenu(fileName = "SequentialSoundCue", menuName = "mis Assets/Audio/SequentialSoundCue")]
    internal sealed class SequentialSoundCue : BaseSoundCue
    {
        protected override SoundCueClipSetting PickClip(AbstractSoundCuePlayer player)
        {
            var state = player.GetOrCreateState<SequenceCueState>();
            var clipIndex = state.NextClipIndex;
            state.NextClipIndex = (clipIndex + 1) % _clips.Length;
            return _clips[clipIndex];
        }
    }
}