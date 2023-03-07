using System;
using mis.Core;

namespace mis.Levels
{
    public sealed class LevelService : ILevelService, IStartableService
    {
        private readonly IConfigService _configService;
        private readonly IMessageService _messageService;
        private readonly IPlayerService _playerService;
        private readonly IObjectService _objectService;
        private readonly IUIService _uiService;

        private GameId _loadedLevelId;

        public int StartPriority => 0;

        public LevelService(
            IConfigService configService,
            IMessageService messageService,
            IPlayerService playerService,
            IObjectService objectService,
            IUIService uIService)
        {
            _configService = configService;
            _messageService = messageService;
            _playerService = playerService;
            _objectService = objectService;
            _uiService = uIService;
        }

        public void OnServiceStart()
        {
            _messageService.Subscribe<StartGameMessage>(OnGameStartMessage);
        }

        public void LoadLevel(GameId levelId)
        {
            if (!_configService.TryGetConfig(levelId, out ILevelConfig config))
            {
                throw new Exception($"Can't load level {levelId}");
            }

            new LoadLevelCommand(
                _loadedLevelId,
                levelId,
                config,
                _messageService,
                _playerService,
                _objectService,
                _uiService).Execute();

            _loadedLevelId = levelId;
        }

        public void ReloadLastLevel()
        {
            if (_loadedLevelId == GameId.ZERO_ID)
            {
                return;
            }
            LoadLevel(_loadedLevelId);
        }

        private void OnGameStartMessage(StartGameMessage obj)
        {
            LoadLevel(GameIds.LEVEL_MAIN_MENU);
        }
    }
}