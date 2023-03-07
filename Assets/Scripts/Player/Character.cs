using UnityEngine;
using mis.Core;

namespace mis.Player
{
    [SelectionBase]
    [RequireComponent(typeof(CharacterMovement))]
    public sealed partial class Character : AbstractGameEntityComponent,
        ICharacter,
        IWeaponOwner
    {
        [SerializeField]
        private CharacterMovement _characterMovement;
        [SerializeField]
        private ViewPoint _viewPoint;
        [SerializeField]
        private BaseResourceContainer _resourceContainer;
        [SerializeField]
        private BaseWeaponHandler _weaponHandler;

        public IViewPoint ViewPoint => _viewPoint;

        public BaseResourceContainer ResourceContainer => _resourceContainer;

        private void Start()
        {
            _weaponHandler.SetOwner(this);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (_characterMovement.IsGrounded)
            {
                ResourceContainer.RefillResource(GameIds.RESOURCE_JUMP);
            }
        }

        private void Reset()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _viewPoint = GetComponent<ViewPoint>();
            _resourceContainer = GetComponent<BaseResourceContainer>();
            _weaponHandler = GetComponent<BaseWeaponHandler>();
        }

        public void OnGainControl() { }

        public void OnLoseControl()
        {
            _weaponHandler.LoseOwner();
        }

        public void TeleportTo(Transform target)
        {
            _characterMovement.SetCollisionEnabled(false);
            transform.position = target.position;
            transform.rotation = target.rotation;
            _characterMovement.SetCollisionEnabled(true);
        }
    }
}
