using mis.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace mis.Player
{
    public class DummyPawn : AbstractGameEntityComponent, IPlayerPawn
    {
        public void OnGainControl() { }

        public void OnLoseControl() { }

        public void TeleportTo(Transform target) { }

        public void OnAltFire(InputAction.CallbackContext context) { }

        public void OnEquipAssaultRifle(InputAction.CallbackContext context) { }

        public void OnEquipBFG(InputAction.CallbackContext context) { }

        public void OnEquipChaingun(InputAction.CallbackContext context) { }

        public void OnEquipCombatShotgun(InputAction.CallbackContext context) { }

        public void OnEquipGaussCannon(InputAction.CallbackContext context) { }

        public void OnEquipLastWeapon(InputAction.CallbackContext context) { }

        public void OnEquipPlasmaGun(InputAction.CallbackContext context) { }

        public void OnEquipRocketLauncher(InputAction.CallbackContext context) { }

        public void OnEquipSuperShotgun(InputAction.CallbackContext context) { }

        public void OnFire(InputAction.CallbackContext context) { }

        public void OnJump(InputAction.CallbackContext context) { }

        public void OnLook(InputAction.CallbackContext context) { }

        public void OnMove(InputAction.CallbackContext context) { }

        public void OnPause(InputAction.CallbackContext context) { }
    }
}