namespace mis.Core
{
    public interface IBattleService : IService
    {
        void StartBattle(GameId battleId, IBattleArena battleAreaInfoProvider);
        void AbortCurrentBattle();
    }
}