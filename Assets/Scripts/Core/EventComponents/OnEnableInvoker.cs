using UnityEngine;
using UnityEngine.Events;

namespace mis.Core
{
    internal sealed class OnEnableInvoker : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _event;

        private void OnEnable() =>
            _event?.Invoke();

        private void Reset() =>
            _event = new UnityEvent();
    }
}