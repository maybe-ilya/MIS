using UnityEngine;
using UnityEngine.Events;

namespace mis.Core
{
    internal sealed class OnTriggerStayInvoker : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<Collider> _event;

        private void OnTriggerStay(Collider other) =>
            _event?.Invoke(other);

        private void Reset() =>
            _event = new UnityEvent<Collider>();
    }
}