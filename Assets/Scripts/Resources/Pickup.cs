using Cysharp.Threading.Tasks;
using mis.Core;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace mis.Resources
{
    [SelectionBase]
    internal sealed class Pickup : MonoBehaviour, IPickup
    {
        [SerializeField]
        private GameId _id;

        [SerializeField]
        [CheckObject]
        private Collider _collider;

        [SerializeField]
        [CheckObject]
        private GameObject _pickupView;

        [SerializeField]
        private bool _canReactivate;

        [SerializeField]
        private float _reactivateDelay;

        [SerializeField]
        private UnityEvent<GameId> _onPickedUp;

        private CancellationTokenSource _cancelTokenSource;

        public GameId GameId => _id;

        public void OnTriggerStay(Collider other)
        {
            if (_id == GameId.ZERO_ID)
            {
                return;
            }

            if (!other.TryGetComponent<GameEntity>(out var gameEntity))
            {
                return;
            }

            var cmd = new ApplyPickupCommand(this, gameEntity);
            cmd.Execute();

            if (cmd.IsSucceed)
            {
                _onPickedUp?.Invoke(_id);

                SetColliderActive(false);
                SetPickupViewVisible(false);

                TryToStartReactivateTimer();
            }
        }

        private void SetPickupViewVisible(bool visible = true)
        {
            if (ReferenceEquals(_pickupView, null))
            {
                return;
            }
            _pickupView.SetActive(visible);
        }

        private void SetColliderActive(bool active = true)
        {
            if (ReferenceEquals(_collider, null))
            {
                return;
            }
            _collider.enabled = active;
        }

        private void TryToStartReactivateTimer()
        {
            if (!_canReactivate)
            {
                return;
            }

            WaitAndReactivate().Forget();
        }

        private async UniTaskVoid WaitAndReactivate()
        {
            CancelWait();
            _cancelTokenSource = new CancellationTokenSource();
            await UniTaskUtils.DelayForSeconds(_reactivateDelay, _cancelTokenSource);

            SetColliderActive(true);
            SetPickupViewVisible(true);
        }

        private void CancelWait()
        {
            _cancelTokenSource?.Cancel();
            _cancelTokenSource = null;
        }

        private void OnEnable()
        {
            SetColliderActive(true);
            SetPickupViewVisible(true);
        }

        private void OnDisable()
        {
            CancelWait();
        }
    }
}