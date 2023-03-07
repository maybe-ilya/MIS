using UnityEditor;
using UnityEngine;

namespace mis.Editor
{
    public static class CursorToggler
    {
        private const string MENU_PATH = "Tools/Toggle Cursor #F1";

        private static bool IsCursorVisible
        {
            get => Cursor.visible || Cursor.lockState == CursorLockMode.None;
            set
            {
                Cursor.visible = value;
                Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
                Menu.SetChecked(MENU_PATH, value);
            }
        }

        [MenuItem(MENU_PATH, validate = true)]
        private static bool CanToggleCursor()
        {
            var result = EditorApplication.isPlaying;
            if (!result)
            {
                IsCursorVisible = false;
            }
            return result;
        }

        [MenuItem(MENU_PATH, validate = false)]
        private static void ToggleCursor()
        {
            IsCursorVisible = !IsCursorVisible;
        }
    }
}