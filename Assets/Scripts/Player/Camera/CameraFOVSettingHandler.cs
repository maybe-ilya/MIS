using mis.Core;
using UnityEngine;

namespace mis.Player
{
    internal sealed class CameraFOVSettingHandler : MonoBehaviour
    {
        [SerializeField]
        private GameId _settingId;

        [SerializeField]
        [CheckObject]
        private Camera _camera;

        private static IGameSettingsService GameSettingsService =>
            GameServices.Get<IGameSettingsService>();

        private static IMessageService MessageService =>
            GameServices.Get<IMessageService>();

        private void OnEnable()
        {
            SetCameraFOV(GameSettingsService.GetFloatSettingValue(_settingId));
            MessageService.Subscribe<FloatSettingChangedMessage>(OnFloatSettingChanged);
        }

        private void OnDisable()
        {
            MessageService.Unsubscribe<FloatSettingChangedMessage>(OnFloatSettingChanged);
        }

        private void SetCameraFOV(float fov) =>
            _camera.fieldOfView = fov;

        private void OnFloatSettingChanged(FloatSettingChangedMessage message)
        {
            if (message.SettingId != _settingId)
            {
                return;
            }
            SetCameraFOV(message.NewValue);
        }
    }
}