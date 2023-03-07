using mis.Core;
using UnityEngine;

namespace mis.GameState
{
    internal class ContinueGameCommand : AbstractCommand
    {
        private readonly IPlayerService _playerService;
        private readonly IUIService _uiService;

        public ContinueGameCommand(IPlayerService playerService, IUIService uiService)
        {
            _playerService = playerService;
            _uiService = uiService;
        }

        protected override void ExecuteInternal()
        {
            _playerService.GetFirstPlayerController().SetPawnInputMode();
            _uiService.CloseWindow(GameIds.UI_PAUSE_WINDOW);
            Time.timeScale = 1;
        }
    }
}