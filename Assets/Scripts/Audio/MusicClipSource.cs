using mis.Core;
using System;
using System.Collections;
using UnityEngine;

namespace mis.Audio
{
    internal sealed class MusicClipSource : AbstractGameEntityComponent, IMusicClipSource
    {
        [SerializeField]
        [CheckObject]
        private AudioSource _audioSource;

        [SerializeField]
        private AnimationCurve _fadeCurve;

        [SerializeField]
        private float _fadeTime;

        private Coroutine _fadeCoroutine;

        public bool IsPlaying => _audioSource.isPlaying;

        public bool IsLooped => _audioSource.loop;

        public void Play(IMusicClipConfig config)
        {
            ApplyConfig(config);
            _audioSource.Play();
        }

        private void ApplyConfig(IMusicClipConfig config)
        {
            _audioSource.clip = config.Clip;
            _audioSource.volume = config.Volume;
            _audioSource.loop = config.IsLooped;
        }

        public void Stop(bool instantly = false)
        {
            if (!IsPlaying)
            {
                return;
            }

            StopFadeCoroutine();
            if (instantly)
            {
                _audioSource.Stop();
            }
            else
            {
                _fadeCoroutine = StartCoroutine(FadeAndStop());
            }
        }

        private void StopFadeCoroutine()
        {
            if (_fadeCoroutine == null)
            {
                return;
            }
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }

        private IEnumerator FadeAndStop()
        {
            var initialVolume = _audioSource.volume;
            var duration = 0.0f;
            var fadeDuration = IsLooped ? _fadeTime : Math.Min(_audioSource.clip.length - _audioSource.time, _fadeTime);

            do
            {
                var timeRatio = Mathf.Clamp(duration / fadeDuration, 0.0f, 1.0f);
                var volumeRatio = _fadeCurve.Evaluate(timeRatio);
                _audioSource.volume = Mathf.Lerp(0.0f, initialVolume, volumeRatio);
                yield return null;
                duration += Time.deltaTime;
            }
            while (duration <= _fadeTime);

            _audioSource.Stop();
        }

        private void OnDisable()
        {
            StopFadeCoroutine();
        }
    }
}