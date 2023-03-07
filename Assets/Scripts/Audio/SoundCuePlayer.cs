using mis.Core;
using UnityEngine;

namespace mis.Audio
{
    internal sealed class SoundCuePlayer : AbstractSoundCuePlayer
    {
        [SerializeField]
        [CheckObject]
        private AudioSource _audioSource;

        [SerializeField]
        [CheckObject]
        private AbstractSoundCue _soundCue;

        private ISoundCueState _cueState;

        private void OnEnable()
        {
            ClearState();
        }

        public override void ApplyCue(AbstractSoundCue cue)
        {
            if (cue == null)
            {
                return;
            }

            _soundCue = cue;
            ClearState();
        }

        public override void Play(AbstractSoundCue cue)
        {
            ApplyCue(cue);
            Play();
        }

        public override void Play()
        {
            if (_soundCue == null)
            {
                return;
            }

            _soundCue.PrepareSource(_audioSource, this);
            _audioSource.Play();
        }

        public override void Stop()
        {
            _audioSource.Stop();
        }

        public override T GetOrCreateState<T>()
        {
            if (_cueState is not T result)
            {
                _cueState = result = new T();
            }
            return result;
        }

        private void ClearState()
        {
            _cueState = null;
        }

        private void Reset()
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}