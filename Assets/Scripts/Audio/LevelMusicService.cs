using mis.Core;

namespace mis.Audio
{
    public sealed class LevelMusicService : ILevelMusicService, IStartableService
    {
        private readonly IMessageService _messageService;
        private readonly IConfigService _configService;
        private readonly IAudioService _audioService;

        private LevelMusicServiceConfig _config;

        public int StartPriority => 0;

        public LevelMusicService(
            IMessageService messageService,
            IConfigService configService,
            IAudioService audioService)
        {
            _messageService = messageService;
            _configService = configService;
            _audioService = audioService;
        }

        public void OnServiceStart()
        {
            _config = _configService.GetConfig<LevelMusicServiceConfig>(GameIds.GLOBAL_LEVEL_MUSIC_CONFIG);
            _messageService.Subscribe<LevelLoadFinishedMessage>(OnLevelLoadFinished);
        }

        private void OnLevelLoadFinished(LevelLoadFinishedMessage message)
        {
            if (!_config.TryGetLevelMusicId(message.LevelId, out var musicId))
            {
                return;
            }
            _audioService.PlayMusic(musicId);
        }
    }
}