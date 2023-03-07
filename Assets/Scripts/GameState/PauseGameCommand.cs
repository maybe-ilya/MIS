using mis.Core;
using UnityEngine;

namespace mis.GameState
{
    internal class PauseGameCommand : AbstractCommand
    {
        private readonly IPlayerService _playerService;
        private readonly IUIService _uiService;

        public PauseGameCommand(IPlayerService playerService, IUIService uiService)
        {
            _playerService = playerService;
            _uiService = uiService;
        }

        protected override void ExecuteInternal()
        {
            _playerService.GetFirstPlayerController().SetUIInputMode();
            _uiService.OpenWindow(GameIds.UI_PAUSE_WINDOW);
            Time.timeScale = 0.0001f;
        }
    }
}