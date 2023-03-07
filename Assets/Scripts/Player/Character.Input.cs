using mis.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using static mis.Core.UnityInputActions;
using static UnityEngine.InputSystem.InputAction;

namespace mis.Player
{
    public partial class Character : IPlayerActions
    {
        private bool CanProcessInput => !Cursor.visible && Cursor.lockState == CursorLockMode.Locked;

        public void OnMove(CallbackContext context)
        {
            var moveVector = CanProcessInput ? context.ReadValue<Vector2>() : Vector2.zero;
            _characterMovement.ApplyMoveInput(moveVector);
        }

        public void OnLook(CallbackContext context)
        {
            var lookVector = CanProcessInput ? context.ReadValue<Vector2>() : Vector2.zero;
            _characterMovement.ApplyLookInput(lookVector);
        }

        public void OnPause(CallbackContext context)
        {
            if (!CanProcessInput || !context.performed)
            {
                return;
            }

            GameServices.Get<IMessageService>().Send<PauseGameMessage>();
        }

        public void OnJump(CallbackContext context)
        {
            if (!CanProcessInput
                || !context.performed
                || !ResourceContainer.HasResource(GameIds.RESOURCE_JUMP))
            {
                return;
            }

            ResourceContainer.RemoveResource(GameIds.RESOURCE_JUMP, 1);
            _characterMovement.ApplyJump();
        }

        public void OnEquipCombatShotgun(CallbackContext context) =>
            EquipWeapon(context, GameIds.WEAPON_COMBAT_SHOTGUN);

        public void OnEquipSuperShotgun(CallbackContext context) =>
            EquipWeapon(context, GameIds.WEAPON_SUPER_SHOTGUN);

        public void OnEquipAssaultRifle(CallbackContext context) =>
            EquipWeapon(context, GameIds.WEAPON_ASSAULT_RIFLE);

        public void OnEquipPlasmaGun(CallbackContext context) =>
            EquipWeapon(context, GameIds.WEAPON_PLASMA_GUN);

        public void OnEquipChaingun(CallbackContext context) =>
            EquipWeapon(context, GameIds.WEAPON_CHAINGUN);

        public void OnEquipGaussCannon(CallbackContext context) =>
            EquipWeapon(context, GameIds.WEAPON_GAUSS_CANNON);

        public void OnEquipRocketLauncher(CallbackContext context) =>
            EquipWeapon(context, GameIds.WEAPON_ROCKET_LAUNCHER);

        public void OnEquipBFG(CallbackContext context) =>
            EquipWeapon(context, GameIds.WEAPON_BFG);

        private void EquipWeapon(CallbackContext context, GameId weaponId)
        {
            if (!CanProcessInput || !context.performed)
            {
                return;
            }
            _weaponHandler.EquipWeapon(weaponId);
        }

        public void OnEquipLastWeapon(CallbackContext context)
        {
            if (!CanProcessInput || !context.performed)
            {
                return;
            }
            _weaponHandler.EquipLastWeapon();
        }

        public void OnFire(CallbackContext context)
        {
            if (!CanProcessInput)
            {
                return;
            }
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    _weaponHandler.StartFire();
                    break;

                case InputActionPhase.Canceled:
                    _weaponHandler.StopFire();
                    break;
            }
        }

        public void OnAltFire(CallbackContext context)
        {
            if (!CanProcessInput)
            {
                return;
            }

            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    _weaponHandler.StartAltFire();
                    break;

                case InputActionPhase.Canceled:
                    _weaponHandler.StopAltFire();
                    break;
            }
        }
    }
}