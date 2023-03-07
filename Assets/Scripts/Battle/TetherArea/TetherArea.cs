using mis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mis.Battle
{
    [ExecuteInEditMode]
    public sealed class TetherArea : MonoBehaviour, IMonsterTetherArea
    {
        [SerializeField]
        [HideInInspector]
        private Transform _cachedTransform;

        [SerializeField]
        [HideInInspector]
        private Vector3[] _points;

        [Header("Area Options")]
        [SerializeField]
        private TetherAreaType _type;

        [SerializeField]
        [Min(0.01f)]
        private float _halfSide;

        [SerializeField]
        [Min(0.01f)]
        private float _pointStep;

        [SerializeField]
        [Min(float.Epsilon)]
        private float _distanceComparisonTolerance;

        [SerializeField]
        private Color _gizmoColor;

        [SerializeField]
        private Color _pointColor;

        [SerializeField]
        [Min(0.01f)]
        private float _pointGizmoSize;

        public Vector3[] Points => _points;

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying || !_cachedTransform.hasChanged)
            {
                return;
            }

            CalculatePointsByAreaType();
            _cachedTransform.hasChanged = false;
        }

        private void OnValidate()
        {
            CalculatePointsByAreaType();
        }

        private void Reset()
        {
            _cachedTransform = transform;
            _type = TetherAreaType.Point;
            CalculatePointsByAreaType();
        }

        private void CalculatePointsByAreaType()
        {
            switch (_type)
            {
                case TetherAreaType.Point:
                    _points = new Vector3[] { _cachedTransform.position };
                    break;

                case TetherAreaType.Square:
                    _points = GetPointsAtSquare(_halfSide);
                    break;

                case TetherAreaType.Circle:
                    _points = GetPointsAtSquare(_halfSide).Where(CheckDistance).ToArray();
                    break;
            }
        }

        private bool CheckDistance(Vector3 point)
        {
            var checkSqrMagnitude = (point - _cachedTransform.position).sqrMagnitude;
            var halfSideSquare = _halfSide * _halfSide;
            return checkSqrMagnitude < halfSideSquare || Math.Abs(checkSqrMagnitude - halfSideSquare) < _distanceComparisonTolerance;
        }

        private Vector3[] GetPointsAtSquare(float halfSide)
        {
            var xVector = _cachedTransform.right;
            var yVector = _cachedTransform.forward;

            var origin = _cachedTransform.position;
            var calcPoint = origin - halfSide * xVector - halfSide * yVector;

            var pointCount = (int)(halfSide * 2 / _pointStep) + 1;

            var result = new List<Vector3>();

            for (var x = 0; x < pointCount; ++x)
            {
                for (var y = 0; y < pointCount; ++y)
                {
                    result.Add(calcPoint + xVector * _pointStep * x + yVector * _pointStep * y);
                }
            }

            return result.ToArray();
        }

        private void OnDrawGizmos()
        {
            switch (_type)
            {
                case TetherAreaType.Square:
                    DrawSquareGizmo();
                    break;

                case TetherAreaType.Circle:
                    DrawCircleGizmo();
                    break;
            }
            DrawPointsGizmos();
        }

        private void DrawSquareGizmo()
        {
            using var _ = new GizmosColorScope(_gizmoColor);
            using var __ = new GizmosMatrixScope(_cachedTransform.localToWorldMatrix);
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(1, 0, 1) * _halfSide * 2);
        }

        private void DrawCircleGizmo()
        {
            using var _ = new GizmosColorScope(_gizmoColor);
            using var __ = new GizmosMatrixScope(_cachedTransform.localToWorldMatrix);
            Gizmos.DrawWireSphere(Vector3.zero, _halfSide);
        }

        private void DrawPointsGizmos()
        {
            using var _ = new GizmosColorScope(_pointColor);
            var gizmoSize = Vector3.one * _pointGizmoSize;

            foreach (var point in _points)
            {
                Gizmos.DrawCube(point, gizmoSize);
            }
        }
#endif
    }
}