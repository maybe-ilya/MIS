using mis.Core;
using UnityEngine;
using UnityEngine.UI;

namespace mis.UI
{
    internal sealed class PauseWindow : AbstractWindow
    {
        [SerializeField]
        [CheckObject]
        private Button _continueButton;

        [SerializeField]
        [CheckObject]
        private Button _settingsButton;

        [SerializeField]
        [CheckObject]
        private Button _mainMenuButton;

        [SerializeField]
        [CheckObject]
        private Button _quitButton;

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
            _uiService = GameServices.Get<IUIService>();
            _levelService = GameServices.Get<ILevelService>();
        }

        private void ClearDependencies()
        {
            _uiService = null;
            _levelService = null;
        }

        private void SubscribeButtons()
        {
            _continueButton.onClick.AddListener(OnContinueButton);
            _settingsButton.onClick.AddListener(OnSettingsButton);
            _mainMenuButton.onClick.AddListener(OnMainMenuButton);
            _quitButton.onClick.AddListener(OnQuitButton);
        }

        private void UnsubscribeButtons()
        {
            _continueButton.onClick.RemoveListener(OnContinueButton);
            _settingsButton.onClick.RemoveListener(OnSettingsButton);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButton);
            _quitButton.onClick.RemoveListener(OnQuitButton);
        }

        private void OnContinueButton()
        {
            MessageService.Send<ContinueGameMessage>();
        }

        private void OnSettingsButton()
        {
            _uiService.OpenWindow(GameIds.UI_SETTINGS_WINDOW);
        }

        private void OnMainMenuButton()
        {
            _levelService.LoadLevel(GameIds.LEVEL_MAIN_MENU);
        }

        private void OnQuitButton()
        {
            MessageService.Send<QuitGameMessage>();
        }
    }
}