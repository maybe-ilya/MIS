using mis.Core;

namespace mis.Resources
{
    public class ApplyPickupCommand : AbstractCommand
    {
        private readonly IPickup _pickup;
        private readonly GameEntity _objectToApply;

        public ApplyPickupCommand(IPickup pickup, GameEntity objectToApply)
        {
            _pickup = pickup;
            _objectToApply = objectToApply;
        }

        protected override void ExecuteInternal()
        {
            if (!_objectToApply.TryGetComponent<BaseResourceContainer>(out var resourceContainer))
            {
                FailCommand();
                return;
            }

            var cfgService = GameServices.Get<IConfigService>();
            if (!cfgService.TryGetConfig<IPickupConfig>(_pickup.GameId, out var pickupConfig))
            {
                FailCommand();
                return;
            }

            var result = false;
            foreach (var resource in pickupConfig.Resources)
            {
                result |= resourceContainer.AddResource(resource);
            }

            if (result)
            {
                SucceedCommand();
            }
            else
            {
                FailCommand();
            }
        }
    }
}