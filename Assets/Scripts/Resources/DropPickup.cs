using mis.Core;
using UnityEngine;

namespace mis.Resources
{
    [SelectionBase]
    internal class DropPickup : AbstractGameEntityComponent, IPickup
    {
        [SerializeField]
        private GameId _id;

        private bool _isAlreadyApplied;

        public GameId GameId => _id;

        private void OnEnable()
        {
            _isAlreadyApplied = false;
        }

        public void OnTriggerStay(Collider other)
        {
            if (_isAlreadyApplied
                || _id == GameId.ZERO_ID
                || !other.TryGetComponent<GameEntity>(out var gameEntity))
            {
                return;
            }

            var cmd = new ApplyPickupCommand(this, gameEntity);
            cmd.Execute();

            if (cmd.IsSucceed)
            {
                _isAlreadyApplied = true;
                GameServices.Get<IObjectService>().DespawnEntity(this);
            }
        }
    }
}