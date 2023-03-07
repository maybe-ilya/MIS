using mis.Core;
using UnityEngine;

namespace mis.Audio
{
    internal abstract class BaseSoundCue : AbstractSoundCue
    {
        [SerializeField]
        protected SoundCueClipSetting[] _clips;

        public override void PrepareSource(AudioSource audioSource, AbstractSoundCuePlayer player)
        {
            ApplyClipSetting(PickClip(player), audioSource);
        }

        protected abstract SoundCueClipSetting PickClip(AbstractSoundCuePlayer player);

        protected void ApplyClipSetting(SoundCueClipSetting clipSetting, AudioSource source)
        {
            source.clip = clipSetting.Clip;
            source.volume = clipSetting.VolumeRange.GetRandonValue();
            source.pitch = clipSetting.PitchRange.GetRandonValue();
        }
    }
}