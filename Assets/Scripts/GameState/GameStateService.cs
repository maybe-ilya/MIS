using mis.Core;
using System;

namespace mis.GameState
{
    public sealed class GameStateService : IGameStateService, IStartableService
    {
        private readonly IMessageService _messageService;
        private readonly IPlayerService _playerService;
        private readonly IAudioService _audioService;
        private readonly IUIService _uiService;

        public int StartPriority => 0;

        public bool IsGamePaused { get; private set; }

        public GameStateService(
            IMessageService messageService,
            IPlayerService playerService,
            IUIService uiService,
            IAudioService audioService)
        {
            _messageService = messageService;
            _playerService = playerService;
            _uiService = uiService;
            _audioService = audioService;
        }

        public void OnServiceStart()
        {
            _messageService.Subscribe<PauseGameMessage>(OnPauseGameMessage);
            _messageService.Subscribe<ContinueGameMessage>(OnUnpauseGameMessage);
            _messageService.Subscribe<QuitGameMessage>(OnQuitGameMessage);
            _messageService.Subscribe<WinGameMessage>(OnWinGameMessage);
            _messageService.Subscribe<LoseGameMessage>(OnLoseGameMessage);
            _messageService.Subscribe<SwitchLevelMessage>(OnSwitchLevelMessage);
        }

        public void ContinueGame()
        {
            IsGamePaused = false;
            new ContinueGameCommand(_playerService, _uiService).Execute();
        }

        public void PauseGame()
        {
            IsGamePaused = true;
            new PauseGameCommand(_playerService, _uiService).Execute();
        }

        public void QuitGame() =>
            new QuitGameCommand().Execute();

        private void OnPauseGameMessage(PauseGameMessage message) =>
            PauseGame();

        private void OnUnpauseGameMessage(ContinueGameMessage message) =>
            ContinueGame();

        private void OnQuitGameMessage(QuitGameMessage message) =>
            QuitGame();

        private void OnSwitchLevelMessage(SwitchLevelMessage message) =>
            ContinueGame();

        private void OnWinGameMessage(WinGameMessage message) =>
            new WinGameCommand(_playerService, _uiService, _audioService).Execute();

        private void OnLoseGameMessage(LoseGameMessage obj) =>
            new LoseGameCommand(_playerService, _uiService, _audioService).Execute();
    }
}