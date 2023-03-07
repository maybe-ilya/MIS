using mis.Core;

namespace mis.GameState
{
    internal sealed class LoseGameCommand : AbstractCommand
    {
        private readonly IPlayerService _playerService;
        private readonly IUIService _uiService;
        private readonly IAudioService _audioService;

        public LoseGameCommand(
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
            _uiService.OpenWindow(GameIds.UI_YOU_LOSE_WINDOW);
            _audioService.PlayMusic(GameIds.MUSIC_DEFEAT_THEME);
        }
    }
}