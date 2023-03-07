namespace mis.Core
{
    public sealed class StartBattleCommand : AbstractCommand
    {
        private readonly GameId _battleId;
        private readonly IBattleArena _battleAreaInfoProvider;

        public StartBattleCommand(GameId battleId, IBattleArena battleAreaInfoProvider)
        {
            _battleId = battleId;
            _battleAreaInfoProvider = battleAreaInfoProvider;
        }

        protected override void ExecuteInternal()
        {
            var battleService = GameServices.Get<IBattleService>();
            battleService.StartBattle(_battleId, _battleAreaInfoProvider);
            SucceedCommand();
        }
    }
}