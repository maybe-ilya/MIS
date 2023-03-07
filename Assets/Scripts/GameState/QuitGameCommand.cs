using mis.Core;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace mis.GameState
{
    internal sealed class QuitGameCommand : AbstractCommand
    {
        protected override void ExecuteInternal()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}