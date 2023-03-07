using mis.Core;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace mis.Levels
{
    internal class LoadLevelCommand : AbstractCommand
    {
        private readonly ILevelConfig _levelConfig;
        private readonly IMessageService _messageService;
        private readonly IPlayerService _playerService;
        private readonly IObjectService _objectService;
        private readonly IUIService _uiService;

        private GameId _previousLevelId, _nextLevelId;

        private const string PLAYER_START_TAG = "PlayerStart";
        private const string PLAYER_PAWN_NAME = "Player Pawn";

        public LoadLevelCommand(
            GameId previousLevelId,
            GameId nextLevelId,
            ILevelConfig levelConfig,
            IMessageService messageService,
            IPlayerService playerService,
            IObjectService objectService,
            IUIService uIService)
        {
            _previousLevelId = previousLevelId;
            _nextLevelId = nextLevelId;
            _levelConfig = levelConfig;
            _messageService = messageService;
            _playerService = playerService;
            _objectService = objectService;
            _uiService = uIService;
        }

        protected override void ExecuteInternal() =>
            TryExecuteAsync();

        protected override async UniTask ExecuteInternalAsync()
        {
            if (_previousLevelId != GameId.ZERO_ID)
            {
                _messageService.Send(new SwitchLevelMessage(_previousLevelId, _nextLevelId));
            }

            var window = _uiService.OpenWindow(GameIds.UI_LOADING_SCREEN_WINDOW);
            var cameraEntity = _objectService.SpawnEntityByType(GameIds.LOADING_SCREEN_CAMERA);

            _messageService.Send(new LevelLoadStartedMessage(_nextLevelId));
            await SceneManager.LoadSceneAsync(_levelConfig.SceneName);
            _messageService.Send(new LevelLoadFinishedMessage(_nextLevelId));

            PrepareLevel();

            _messageService.Send(new LevelStartedMessage(_nextLevelId));

            _objectService.DespawnEntity(cameraEntity);
            window.Close();

            SucceedCommand();
        }

        private void PrepareLevel()
        {
            var player = _playerService.GetFirstPlayer();

            var newPawn = _objectService.SpawnEntityByType<IPlayerPawn>(_levelConfig.PlayerPawnId, name: PLAYER_PAWN_NAME);
            player.Controller.ControlPawn(newPawn);
            player.Controller.SetPawnInputMode();

            var playerStart = GameObject.FindGameObjectWithTag(PLAYER_START_TAG);
            if (playerStart != null)
            {
                newPawn.TeleportTo(playerStart.transform);
            }

            _uiService.OpenHUD(_levelConfig.HUDId);
        }
    }
}