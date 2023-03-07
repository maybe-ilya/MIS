using mis.Core;

namespace mis.Messages
{
    public class MainMenuStartedHandler : AbstractMessageHandler<LevelStartedMessage>
    {
        private readonly IUIService _uiService;
        private readonly IPlayerService _playerService;

        public MainMenuStartedHandler(
            IMessageService messageService,
            IUIService uIService,
            IPlayerService playerService) : base(messageService)
        {
            _uiService = uIService;
            _playerService = playerService;
        }

        protected override void OnMessageInvoked(LevelStartedMessage message)
        {
            if (message.LevelId != GameIds.LEVEL_MAIN_MENU)
            {
                return;
            }

            _uiService.OpenWindow(GameIds.UI_MAIN_MENU_WINDOW);
            _playerService.GetFirstPlayerController().SetUIInputMode();
        }
    }
}