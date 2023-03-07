using mis.Core;
using UnityEngine;

namespace mis.Resources
{
    public class DropPickupImage : MonoBehaviour
    {
        [SerializeField]
        [CheckObject]
        private Transform _transform, _parentTransform;

        [SerializeField]
        private Vector3 _positionOffset;

        [SerializeField]
        [Tooltip("Ћюбое ненулевое значение означает сброс соответвующей координаты поворота по Ёйлеру до нул€")]
        private Vector3Int _freezeAxisData;

        private bool _isVisible;

        private void OnBecameVisible() =>
            _isVisible = true;

        private void OnBecameInvisible() =>
            _isVisible = false;

        private void LateUpdate()
        {
            if (!_isVisible) return;

            var cameraPosition = Camera.main.transform.position;
            var position = _parentTransform.position + _positionOffset;
            var rotation = Quaternion.LookRotation(cameraPosition - position, Vector3.up).eulerAngles;

            for (var coorIndex = 0; coorIndex <= 2; ++coorIndex)
            {
                rotation[coorIndex] = _freezeAxisData[coorIndex] != 0 ? 0.0f : rotation[coorIndex];
            }

            _transform.position = position;
            _transform.rotation = Quaternion.Euler(rotation);
        }

        private void Reset()
        {
            _transform = transform;
            _parentTransform = _transform.parent;
        }
    }
}