using mis.Core;

namespace mis.Drop
{
    public sealed class DropService : IDropService, IStartableService
    {
        private readonly IObjectService _objectService;
        private readonly IMessageService _messageService;
        private readonly IConfigService _configService;

        private IDropServiceConfig _config;

        public int StartPriority => 0;

        public DropService(
            IObjectService objectService,
            IMessageService messageService,
            IConfigService configService)
        {
            _objectService = objectService;
            _messageService = messageService;
            _configService = configService;
        }

        public void OnServiceStart()
        {
            _config = _configService.GetConfig<IDropServiceConfig>(GameIds.GLOBAL_DROP_CONFIG);
            _messageService.Subscribe<GameEntityKilledMessage>(OnEntityKilled);
        }

        private void OnEntityKilled(GameEntityKilledMessage message)
        {
            if (!_config.TryGetDropData(message.KilledEntity.TypeId, out var dropdata))
            {
                return;
            }

            new DropResourcesCommand(
                dropdata, 
                _objectService, 
                message.KilledEntity.transform.position,
                _config.PointData).Execute();
        }
    }
}