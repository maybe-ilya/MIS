using UnityEngine.InputSystem;

namespace mis.Core
{
    public static class InputSystemExtensions
    {
        public static InputActionReference GetActionReference(this InputAction inputAction) =>
            InputActionReference.Create(inputAction);
    }
}