namespace mis.Core
{
    public interface IBattleHandler
    {
        GameId BattleId { get; }
        void StartBattle();
        void AbortBattle();
    }
}