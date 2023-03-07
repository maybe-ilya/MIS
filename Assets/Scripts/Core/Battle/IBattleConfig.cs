namespace mis.Core
{
    public interface IBattleConfig : IConfig
    {
        int WaveCount { get; }
        IBattleWaveData[] Waves { get; }
    }
}