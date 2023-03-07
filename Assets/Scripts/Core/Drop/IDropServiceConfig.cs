namespace mis.Core
{
    public interface IDropServiceConfig : IConfig
    {
        DropPointData PointData { get; }
        bool TryGetDropData(GameId entityTypeId, out IDropData dropData);
    }
}