namespace mis.Core
{
    public interface IDamageConfig : IConfig
    {
        float ArmorDefenceRatio { get; }

        float ArmorDefencePower { get; }
    }
}