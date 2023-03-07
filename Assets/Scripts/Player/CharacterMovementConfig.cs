using UnityEngine;

namespace mis.Core
{
    [CreateAssetMenu(menuName = "mis Assets/" + nameof(CharacterMovementConfig))]
    public class CharacterMovementConfig : ScriptableObject
    {
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private float _horizontalLookSpeed;
        [SerializeField]
        private float _verticalLookSpeed;
        [SerializeField]
        private FloatRange _verticalLookRange = (-90, 90);
        [SerializeField]
        private bool _isVerticalLookInverted;
        [SerializeField]
        private float _gravityStrenght;
        [SerializeField]
        private float _jumpForce;
        [SerializeField]
        private float _acceleration;
        [SerializeField]
        private float _accelerationInAir;
        [SerializeField]
        private float _deceleration;
        [SerializeField]
        private float _decelerationInAir;
        [SerializeField]
        private GameId _horizontalSensitivityScaleSettingId;
        [SerializeField]
        private GameId _verticalSensitivityScaleSettingId;
        [SerializeField]
        private GameId _verticalAxisInversionSettingId;

        public float MoveSpeed => _moveSpeed;
        public float HorizontalLookSpeed => _horizontalLookSpeed;
        public float VerticalLookSpeed => _verticalLookSpeed;
        public FloatRange VerticalLookRange => _verticalLookRange;
        public bool IsVerticalLookInverted => _isVerticalLookInverted;
        public float GravityStrenght => _gravityStrenght;
        public float JumpForce => _jumpForce;
        public float Acceleration => _acceleration;
        public float AccelerationInAir => _accelerationInAir;
        public float Deceleration => _deceleration;
        public float DecelerationInAir => _decelerationInAir;
        public GameId HorizontalSensitivityScaleSettingId => _horizontalSensitivityScaleSettingId;
        public GameId VerticalSensitivityScaleSettingId => _verticalSensitivityScaleSettingId;
        public GameId VerticalAxisInversionSettingId => _verticalAxisInversionSettingId;
    }
}
