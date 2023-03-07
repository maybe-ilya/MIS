using mis.Core;
using UnityEngine;

namespace mis.Player
{
    public class ViewPoint : MonoBehaviour, IViewPoint
    {
        [SerializeField]
        private Transform _transform;

        public Vector3 Position => _transform.position;

        public Vector3 ForwardDirection => _transform.forward;

        public Vector3 UpDirection => _transform.up;

        public Vector3 RightDirection => _transform.right;
    }
}