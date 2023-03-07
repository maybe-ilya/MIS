using mis.Core;

namespace mis.Battle
{
    public sealed class BattleService : IBattleService, IStartableService
    {
        private readonly IConfigService _configService;
        private readonly IPlayerService _playerService;
        private readonly IMessageService _messageService;
        private IBattleHandler _currentBattleHandler;

        public int StartPriority => 0;

        public BattleService(
            IConfigService configService,
            IPlayerService playerService,
            IMessageService messageService)
        {
            _configService = configService;
            _playerService = playerService;
            _messageService = messageService;
        }

        public void OnServiceStart()
        {
            _messageService.Subscribe<SwitchLevelMessage>(OnSwitchLevel);
        }

        public void StartBattle(GameId battleId, IBattleArena battleAreaInfoProvider)
        {
            AbortCurrentBattle();

            var target = _playerService.GetFirstPlayer().Controller.Pawn.Entity;
            var battleConfig = _configService.GetConfig<IBattleConfig>(battleId);

            _currentBattleHandler = new BattleHandler(
                battleConfig,
                target,
                battleAreaInfoProvider,
                _configService,
                _messageService);
            _currentBattleHandler.StartBattle();
        }

        public void AbortCurrentBattle()
        {
            if (_currentBattleHandler == null)
            {
                return;
            }

            _currentBattleHandler.AbortBattle();
            _currentBattleHandler = null;
        }

        private void OnSwitchLevel(SwitchLevelMessage obj)
        {
            AbortCurrentBattle();
        }
    }
}