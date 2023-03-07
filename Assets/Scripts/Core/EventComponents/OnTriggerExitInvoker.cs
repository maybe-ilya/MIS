using UnityEngine;
using UnityEngine.Events;

namespace mis.Core
{
    internal sealed class OnTriggerExitInvoker : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<Collider> _event;

        private void OnTriggerExit(Collider other) =>
            _event?.Invoke(other);

        private void Reset() =>
            _event = new UnityEvent<Collider>();
    }
}