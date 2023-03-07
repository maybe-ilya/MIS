using UnityEngine;
using mis.Core;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace mis.Player
{
    public class InputController : MonoBehaviour
    {
        private IPlayerPawn _pawn;
        private UnityInputActions _inputActions;

        private UnityInputActions.PlayerActions PlayerActions => _inputActions.Player;

        private UnityInputActions.UIActions UIActions => _inputActions.UI;

        private static InputSystemUIInputModule UIInputModule =>
            EventSystem.current.currentInputModule as InputSystemUIInputModule;

        private void Awake()
        {
            _inputActions = new UnityInputActions();
        }

        public void SetPawn(IPlayerPawn pawn)
        {
            _pawn = pawn;
            PlayerActions.SetCallbacks(_pawn);
        }

        public void SetPawnInputMode()
        {
            DeactivateUIControls();
            ActivatePawnControls();
        }

        public void SetUIInputMode()
        {
            DeactivatePawnControls();
            ActivateUIControls();
        }

        private void ActivatePawnControls()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerActions.Enable();
        }

        private void ActivateUIControls()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            BindUIInputModule();
            UIActions.Enable();
        }

        private void DeactivatePawnControls()
        {
            PlayerActions.Disable();
        }

        private void DeactivateUIControls()
        {
            UIInputModule?.UnassignActions();
            UIActions.Disable();
        }

        private void BindUIInputModule()
        {
            var uiInputModule = UIInputModule;
            uiInputModule.UnassignActions();
            uiInputModule.submit = UIActions.Submit.GetActionReference();
            uiInputModule.cancel = UIActions.Cancel.GetActionReference();
            uiInputModule.move = UIActions.Navigate.GetActionReference();
            uiInputModule.leftClick = UIActions.Click.GetActionReference();
            uiInputModule.point = UIActions.Cursor.GetActionReference();
            uiInputModule.scrollWheel = UIActions.Scroll.GetActionReference();
        }
    }
}
