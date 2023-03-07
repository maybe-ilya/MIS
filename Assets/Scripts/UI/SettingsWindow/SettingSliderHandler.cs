using mis.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace mis.UI
{
    internal sealed class SettingSliderHandler : MonoBehaviour, IMoveHandler
    {
        [SerializeField]
        private GameId _settingId;

        [SerializeField]
        [CheckObject]
        private Slider _slider;

        [SerializeField]
        [CheckObject]
        private TMP_Text _text;

        [SerializeField]
        private float _valueStep = 0.1f;

        private static IGameSettingsService GameSettingsService =>
            GameServices.Get<IGameSettingsService>();

        private void OnEnable()
        {
            var settingRange = GameSettingsService.GetFloatSettingLimit(_settingId);
            _slider.minValue = settingRange.Min;
            _slider.maxValue = settingRange.Max;

            var settingValue = GameSettingsService.GetFloatSettingValue(_settingId);
            _slider.value = settingValue;
            _text.text = settingValue.ToString();

            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            _text.text = GameSettingsService.SetFloatSettingValue(_settingId, value).ToString();
        }

        public void OnMove(AxisEventData eventData)
        {
            switch (eventData.moveDir)
            {
                case MoveDirection.Left:
                case MoveDirection.Right:
                    _slider.value += Math.Sign(eventData.moveVector.x) * _valueStep;
                    break;
            }
        }
    }
}