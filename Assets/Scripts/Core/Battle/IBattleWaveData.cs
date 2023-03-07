namespace mis.Core
{
    public interface IBattleWaveData
    {
        int MaxConcurrentMonsters { get; }
        GameId[] MonstersToSpawn { get; }
    }
}