using System;
using UnityEngine;

namespace mis.Core
{
    public sealed class GizmosMatrixScope : IDisposable
    {
        private readonly Matrix4x4 _originalMatrix;

        public GizmosMatrixScope(Matrix4x4 newMatrix)
        {
            _originalMatrix = Gizmos.matrix;
            Gizmos.matrix = newMatrix;
        }

        public void Dispose()
        {
            Gizmos.matrix = _originalMatrix;
        }
    }

    public sealed class GizmosColorScope : IDisposable
    {
        private readonly Color _originalColor;

        public GizmosColorScope(Color newColor)
        {
            _originalColor = Gizmos.color;
            Gizmos.color = newColor;
        }

        public void Dispose()
        {
            Gizmos.color = _originalColor;
        }
    }
}