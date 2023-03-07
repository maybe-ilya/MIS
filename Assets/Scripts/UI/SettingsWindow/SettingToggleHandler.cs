using mis.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace mis.UI
{
    internal sealed class SettingToggleHandler : MonoBehaviour, ISubmitHandler
    {
        [SerializeField]
        private GameId _settingId;

        [SerializeField]
        [CheckObject]
        private Toggle _toggle;

        private static IGameSettingsService GameSettingsService =>
            GameServices.Get<IGameSettingsService>();

        private void OnEnable()
        {
            _toggle.isOn = GameSettingsService.GetBoolSettingValue(_settingId);
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool newValue)
        {
            GameSettingsService.SetBoolSettingValue(_settingId, newValue);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            _toggle.isOn = !_toggle.isOn;
        }
    }
}