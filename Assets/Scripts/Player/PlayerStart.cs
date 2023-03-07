using mis.Core;
using UnityEngine;

namespace mis.Player
{
    [SelectionBase]
    internal sealed class PlayerStart : MonoBehaviour
    {
        [SerializeField]
        private Color _meshGizmoColor;
        [SerializeField]
        private Color _wireMeshGizmoColor;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            using var _1 = new GizmosMatrixScope(transform.localToWorldMatrix);

            var mesh = PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Capsule);

            using var _2 = new GizmosColorScope(_meshGizmoColor);
            Gizmos.DrawMesh(mesh);

            using var _3 = new GizmosColorScope(_wireMeshGizmoColor);
            Gizmos.DrawWireMesh(mesh);
        }
#endif
    }
}