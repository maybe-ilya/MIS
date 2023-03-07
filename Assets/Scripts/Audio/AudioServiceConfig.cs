using mis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace mis.Audio
{
    [CreateAssetMenu(fileName = "AudioServiceConfig", menuName = "mis Assets/AudioServiceConfig")]
    internal sealed class AudioServiceConfig :
        AbstractConfig,
        IAudioServiceConfig,
        ISerializationCallbackReceiver
    {
        [Serializable]
        private struct SettingIdToSoundChannel
        {
            public GameId SettingId;
            public string ChannelName;
        }

        [SerializeField]
        [CheckObject]
        private AudioMixer _mainAudioMixer;

        [SerializeField]
        private GameId _musicSourceId;

        [SerializeField]
        private FloatRange _channelDBRange;

        [SerializeField]
        [OneLine]
        private SettingIdToSoundChannel[] _settingToChannelData;

        private Dictionary<GameId, string> _settingToChannelMap;

        public AudioMixer MainMixer => _mainAudioMixer;

        public FloatRange ChannelDBRange => _channelDBRange;

        public GameId MusicSourceId => _musicSourceId;

        public bool IsSoundSetting(GameId settingId) =>
            _settingToChannelMap.ContainsKey(settingId);

        public string GetSoundChannel(GameId settingId) =>
            _settingToChannelMap.TryGetValue(settingId, out var channel) ? channel : string.Empty;

        public (GameId settingId, string soundChannel)[] GetSoundSettingData() =>
            _settingToChannelMap.Select(keyPair => (keyPair.Key, keyPair.Value)).ToArray();

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            _settingToChannelMap = _settingToChannelData.ToDictionary(entry => entry.SettingId, entry => entry.ChannelName);
        }
    }
}