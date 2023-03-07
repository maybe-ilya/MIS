using System;
using UnityEngine;

namespace mis.Core
{
    public sealed class GUIColorScope : IDisposable
    {
        private readonly Color _originalColor;

        public GUIColorScope(Color guiColor)
        {
            _originalColor = GUI.color;
            GUI.color = guiColor;
        }

        public void Dispose()
        {
            GUI.color = _originalColor;
        }
    }

    public sealed class GUIBackgroundColorScope : IDisposable
    {
        private readonly Color _originalColor;

        public GUIBackgroundColorScope(Color guiColor)
        {
            _originalColor = GUI.backgroundColor;
            GUI.backgroundColor = guiColor;
        }

        public void Dispose()
        {
            GUI.backgroundColor = _originalColor;
        }
    }

    public sealed class GUIContentColorScope : IDisposable
    {
        private readonly Color _originalColor;

        public GUIContentColorScope(Color guiColor)
        {
            _originalColor = GUI.contentColor;
            GUI.contentColor = guiColor;
        }

        public void Dispose()
        {
            GUI.contentColor = _originalColor;
        }
    }
}