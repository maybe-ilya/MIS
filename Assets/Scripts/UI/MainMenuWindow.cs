using mis.Core;
using UnityEngine;
using UnityEngine.UI;

namespace mis.UI
{
    internal sealed class MainMenuWindow : AbstractWindow
    {
        [SerializeField]
        [CheckObject]
        private Button _startDemoButton;

        [SerializeField]
        [CheckObject]
        private Button _settingButton;

        [SerializeField]
        [CheckObject]
        private Button _quitButton;

        private IMessageService _messageService;
        private IUIService _uiService;
        private ILevelService _levelService;

        protected override void OnOpened()
        {
            GatherDependencies();
            SubscribeButtons();
        }

        protected override void OnClosed()
        {
            UnsubscribeButtons();
            ClearDependencies();
        }

        private void GatherDependencies()
        {
            _messageService = GameServices.Get<IMessageService>();
            _uiService = GameServices.Get<IUIService>();
            _levelService = GameServices.Get<ILevelService>();
        }

        private void ClearDependencies()
        {
            _messageService = null;
            _uiService = null;
            _levelService = null;
        }

        private void SubscribeButtons()
        {
            _startDemoButton.onClick.AddListener(OnStartDemoButtonClick);
            _settingButton.onClick.AddListener(OnSettingsButtonClick);
            _quitButton.onClick.AddListener(OnQuitButtonClick);
        }

        private void UnsubscribeButtons()
        {
            _startDemoButton.onClick.RemoveListener(OnStartDemoButtonClick);
            _settingButton.onClick.RemoveListener(OnSettingsButtonClick);
            _quitButton.onClick.RemoveListener(OnQuitButtonClick);
        }

        private void OnStartDemoButtonClick()
        {
            _levelService.LoadLevel(GameIds.LEVEL_DEMO);
        }

        private void OnSettingsButtonClick()
        {
            _uiService.OpenWindow(GameIds.UI_SETTINGS_WINDOW);
        }

        private void OnQuitButtonClick()
        {
            _messageService.Send<QuitGameMessage>();
        }
    }
}