namespace mis.Core
{
    public interface IConfigService : IService
    {
        T GetConfig<T>(GameId configId) where T : class, IConfig;
        bool IsConfigExists<T>(GameId configId) where T : class, IConfig;
        bool TryGetConfig<T>(GameId configId, out T config) where T : class, IConfig;
    }
}
