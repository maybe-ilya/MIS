using UnityEngine;
using UnityEngine.Events;

namespace mis.Core
{
    internal sealed class OnDisableInvoker : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _event;

        private void OnDisable() =>
            _event?.Invoke();

        private void Reset() => 
            _event = new UnityEvent();
    }
}