using mis.Core;
using UnityEngine;
using UnityEngine.UI;

namespace mis.UI
{
    internal sealed class SettingsWindow : AbstractWindow
    {
        [SerializeField]
        [CheckObject]
        private Button _closeButton;

        protected override void OnOpened()
        {
            _closeButton.onClick.AddListener(OnCloseButton);
        }

        protected override void OnClosed()
        {
            _closeButton.onClick.RemoveListener(OnCloseButton);
        }

        private void OnCloseButton() => Close();
    }
}