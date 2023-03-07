using mis.Core;

namespace mis.GameState
{
    internal sealed class WinGameCommand : AbstractCommand
    {
        private readonly IPlayerService _playerService;
        private readonly IUIService _uiService;
        private readonly IAudioService _audioService;

        public WinGameCommand(
            IPlayerService playerService, 
            IUIService uiService,
            IAudioService audioService)
        {
            _playerService = playerService;
            _uiService = uiService;
            _audioService = audioService;
        }

        protected override void ExecuteInternal()
        {
            _playerService.GetFirstPlayerController().SetUIInputMode();
            _uiService.OpenWindow(GameIds.UI_YOU_WIN_WINDOW);
            _audioService.PlayMusic(GameIds.MUSIC_VICTORY_THEME);
        }
    }
}