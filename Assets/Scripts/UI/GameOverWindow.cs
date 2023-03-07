using mis.Core;
using UnityEngine;
using UnityEngine.UI;

namespace mis.UI
{
    internal sealed class GameOverWindow : AbstractWindow
    {
        [SerializeField]
        [CheckObject]
        private Button _replayButton;

        [SerializeField]
        [CheckObject]
        private Button _mainMenuButton;

        [SerializeField]
        [CheckObject]
        private Button _quitButton;

        private IMessageService _messageService;
        private ILevelService _levelService;

        protected override void OnOpened()
        {
            GatherDependencies();
            SubstribeButtons();
        }

        protected override void OnClosed()
        {
            UnsubscribeButtons();
            ClearDependencies();
        }

        private void GatherDependencies()
        {
            _messageService = GameServices.Get<IMessageService>();
            _levelService = GameServices.Get<ILevelService>();
        }

        private void ClearDependencies()
        {
            _messageService = null;
            _levelService = null;
        }

        private void SubstribeButtons()
        {
            _replayButton.onClick.AddListener(OnReplayButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainButtonClick);
            _quitButton.onClick.AddListener(OnQuitButtonClick);
        }

        private void UnsubscribeButtons()
        {
            _replayButton.onClick.RemoveListener(OnReplayButtonClick);
            _mainMenuButton.onClick.RemoveListener(OnMainButtonClick);
            _quitButton.onClick.RemoveListener(OnQuitButtonClick);
        }

        private void OnReplayButtonClick() =>
            _levelService.ReloadLastLevel();

        private void OnMainButtonClick() =>
            _levelService.LoadLevel(GameIds.LEVEL_MAIN_MENU);

        private void OnQuitButtonClick() =>
            _messageService.Send<QuitGameMessage>();
    }
}