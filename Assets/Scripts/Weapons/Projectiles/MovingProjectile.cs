using UnityEngine;
using UnityEngine.Events;

namespace mis.Weapons
{
    public sealed class MovingProjectile : AbstractProjectile
    {
        [SerializeField]
        private float _speed;

        UnityEvent _event;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public override void Setup(Vector3 origin, Vector3 target)
        {
            _transform.position = origin;
            _transform.forward = (target - origin).normalized;
        }

        private void Update()
        {
            _transform.position += _transform.forward * _speed * Time.deltaTime;
        }
    }
}