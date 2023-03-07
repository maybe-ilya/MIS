using mis.Core;

namespace mis.Audio
{
    public sealed class AudioService : IAudioService, IStartableService
    {
        private readonly IConfigService _configService;
        private readonly IGameSettingsService _gameSettingsService;
        private readonly IMessageService _messageService;
        private readonly IObjectService _objectService;

        private AudioServiceConfig _config;
        private IMusicClipSource[] _musicSources;
        private const int MUSIC_SOURCES_COUNT = 2;

        public int StartPriority => 0;

        public AudioService(
            IConfigService configService,
            IGameSettingsService gameSettingsService,
            IMessageService messageService,
            IObjectService objectService)
        {
            _configService = configService;
            _gameSettingsService = gameSettingsService;
            _messageService = messageService;
            _objectService = objectService;
        }

        public void OnServiceStart()
        {
            _config = _configService.GetConfig<AudioServiceConfig>(GameIds.GLOBAL_AUDIO_CONFIG);
            _messageService.Subscribe<FloatSettingChangedMessage>(OnFloatSettingChangedMessage);

            _musicSources = new IMusicClipSource[MUSIC_SOURCES_COUNT];
            for (var index = 0; index < MUSIC_SOURCES_COUNT; ++index)
            {
                _musicSources[index] = _objectService.SpawnEntityByType<IMusicClipSource>(_config.MusicSourceId);
            }

            InitVolumes();
        }

        private void InitVolumes()
        {
            foreach ((var settingId, var soundChannel) in _config.GetSoundSettingData())
            {
                SetVolume(soundChannel, _gameSettingsService.GetFloatSettingValue(settingId));
            }
        }

        private void SetVolume(string channelName, float volume) =>
            _config.MainMixer.SetFloat(channelName, _config.ChannelDBRange.Lerp(volume));

        private void OnFloatSettingChangedMessage(FloatSettingChangedMessage message)
        {
            var settingId = message.SettingId;
            if (!_config.IsSoundSetting(settingId))
            {
                return;
            }

            SetVolume(_config.GetSoundChannel(settingId), message.NewValue);
        }

        public void PlayMusic(GameId musicId)
        {
            var musicConfig = _configService.GetConfig<IMusicClipConfig>(musicId);
            var isPlayStarted = false;

            for (var index = 0; index < MUSIC_SOURCES_COUNT; ++index)
            {
                if (!_musicSources[index].IsPlaying)
                {
                    _musicSources[index].Play(musicConfig);
                    isPlayStarted = true;
                }
                else
                {
                    _musicSources[index].Stop();
                }
            }

            if (!isPlayStarted)
            {
                _musicSources[0].Stop(true);
                _musicSources[0].Play(musicConfig);
            }
        }
    }
}
