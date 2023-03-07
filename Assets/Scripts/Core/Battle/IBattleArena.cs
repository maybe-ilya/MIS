namespace mis.Core
{
    public interface IBattleArena
    {
        ITetherAreaInfoProvider TetherAreaInfo { get; }
        IMonsterSpawner[] Spawners { get; }
        void OnBattleStart();
        void OnBattleStop();
    }
}