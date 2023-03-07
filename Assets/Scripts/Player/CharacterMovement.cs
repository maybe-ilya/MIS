using System;
using UnityEngine;

namespace mis.Core
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform _cachedTransform;
        [SerializeField]
        private Transform _viewPointTransform;
        [SerializeField]
        private CharacterMovementConfig _movementConfig;
        [SerializeField]
        private CharacterController _characterController;

        private Vector3 _moveInput;
        private Vector2 _lookInput;
        private bool _isMovePerformed;

        private const float GRAVITY = -9.81f;

        private Vector3 _characterRotation, _viewPointRotation;
        private float _verticalVelocity;

        private float _currentSpeed;

        private float _horizontalLookScale, _verticalLookScale;
        private bool _isVerticalAxisInverted;

        public bool IsGrounded => _characterController.isGrounded;

        private static IGameSettingsService GameSettingsService =>
            GameServices.Get<IGameSettingsService>();

        private static IMessageService MessageService =>
            GameServices.Get<IMessageService>();

        public void ApplyMoveInput(Vector2 input)
        {
            _isMovePerformed = input != Vector2.zero;
            if (_isMovePerformed)
            {
                _moveInput = new Vector3(input.x, 0.0f, input.y);
            }
        }

        public void ApplyLookInput(Vector2 input)
        {
            _lookInput = input;
        }

        public void ApplyJump()
        {
            _verticalVelocity = _movementConfig.JumpForce;
        }

        public void SetCollisionEnabled(bool enabled)
        {
            _characterController.enabled = enabled;
        }

        private void Reset()
        {
            _cachedTransform = transform;
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            SubscribeToMessages();
            GetInitialSettings();
        }

        private void OnDisable()
        {
            UnsubscribeFromMessages();
            ClearData();
        }

        private void Start()
        {
            _characterRotation = Vector3.zero;
            _viewPointRotation = Vector3.zero;
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            UpdateSpeed(deltaTime);
            ApplyGravity(deltaTime);
            ApplyMovement(deltaTime);
            ApplyRotation(deltaTime);
        }

        private void UpdateSpeed(float deltaTime)
        {
            _currentSpeed = _isMovePerformed
                ? Math.Min(_currentSpeed + (IsGrounded ? _movementConfig.Acceleration : _movementConfig.AccelerationInAir) * deltaTime, _movementConfig.MoveSpeed)
                : Math.Max(_currentSpeed - (IsGrounded ? _movementConfig.Deceleration : _movementConfig.DecelerationInAir) * deltaTime, 0.0f);
        }

        private void ApplyGravity(float deltaTime)
        {
            if (IsGrounded && _verticalVelocity < 0.0f)
            {
                _verticalVelocity = 0.0f;
            }
            _verticalVelocity += GRAVITY * _movementConfig.GravityStrenght * deltaTime;
        }

        private void ApplyMovement(float deltaTime)
        {
            var move = _moveInput.normalized * _currentSpeed;
            move = _cachedTransform.TransformDirection(move);
            move.y = _verticalVelocity;

            _characterController.Move(move * deltaTime);
        }

        private void ApplyRotation(float deltaTime)
        {
            _characterRotation.y += _lookInput.x * _movementConfig.HorizontalLookSpeed * _horizontalLookScale * deltaTime;
            _cachedTransform.localRotation = Quaternion.Euler(_characterRotation);

            _viewPointRotation.x += _lookInput.y * (_isVerticalAxisInverted ? -1 : 1) * _movementConfig.VerticalLookSpeed * _verticalLookScale * deltaTime;
            _viewPointRotation.x = _movementConfig.VerticalLookRange.Clamp(_viewPointRotation.x);
            _viewPointTransform.localRotation = Quaternion.Euler(_viewPointRotation);
        }

        private void GetInitialSettings()
        {
            _horizontalLookScale = GameSettingsService.GetFloatSettingValue(_movementConfig.HorizontalSensitivityScaleSettingId);
            _verticalLookScale = GameSettingsService.GetFloatSettingValue(_movementConfig.VerticalSensitivityScaleSettingId);
            _isVerticalAxisInverted = GameSettingsService.GetBoolSettingValue(_movementConfig.VerticalAxisInversionSettingId);
        }

        private void SubscribeToMessages()
        {
            MessageService.Subscribe<FloatSettingChangedMessage>(OnFloatSettingChanged);
            MessageService.Subscribe<BoolSettingChangedMessage>(OnBoolSettingChanged);
        }

        private void UnsubscribeFromMessages()
        {
            MessageService.Unsubscribe<FloatSettingChangedMessage>(OnFloatSettingChanged);
            MessageService.Unsubscribe<BoolSettingChangedMessage>(OnBoolSettingChanged);
        }

        private void OnBoolSettingChanged(BoolSettingChangedMessage message)
        {
            if (message.SettingId != _movementConfig.VerticalAxisInversionSettingId)
            {
                return;
            }

            _isVerticalAxisInverted = message.NewValue;
        }

        private void OnFloatSettingChanged(FloatSettingChangedMessage message)
        {
            if (message.SettingId == _movementConfig.HorizontalSensitivityScaleSettingId)
            {
                _horizontalLookScale = message.NewValue;
            }
            else if (message.SettingId == _movementConfig.VerticalSensitivityScaleSettingId)
            {
                _verticalLookScale = message.NewValue;
            }
        }

        private void ClearData()
        {
            _moveInput = Vector3.zero;
            _lookInput = Vector2.zero;
            _isMovePerformed = false;
            _characterRotation = Vector3.zero;
            _viewPointRotation = Vector3.zero;
            _verticalVelocity = 0.0f;
            _currentSpeed = 0.0f;
            _horizontalLookScale = 0.0f; _verticalLookScale = 0.0f;
            _isVerticalAxisInverted = false;
        }
    }
}
