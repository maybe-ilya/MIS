using mis.Core;
using UnityEngine;

namespace mis.Battle
{
    internal sealed class MonsterSpawner : MonoBehaviour, IMonsterSpawner
    {
        [SerializeField]
        private Transform _cachedTransform;

        [Header("Gizmo Settings")]
        [SerializeField]
        private Color _mainGizmoColor;
        [SerializeField]
        private Color _additionalGizmoColor;

        public Transform SpawnerTransform => _cachedTransform;

        private void Reset() =>
            _cachedTransform = transform;

        private void OnDrawGizmos()
        {
            var position = Vector3.up * 0.5f;
            var size = Vector3.one;

            using var __ = new GizmosMatrixScope(_cachedTransform.localToWorldMatrix);

            using (new GizmosColorScope(_mainGizmoColor))
            {
                Gizmos.DrawCube(position, size);
            }
            using (new GizmosColorScope(_additionalGizmoColor))
            {
                Gizmos.DrawWireCube(position, size);
            }
        }
    }
}