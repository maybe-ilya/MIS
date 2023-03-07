using mis.Core;
using System.Collections.Generic;
using UnityEngine;

namespace mis.Configs
{
    //  Using Resources for now, but definitely this needs to be replaced with Addressables
    public class ResourcesConfigService : IConfigService, IStartableService
    {
        private readonly Dictionary<GameId, IConfig> _configs;

        public int StartPriority => int.MinValue;

        public ResourcesConfigService()
        {
            _configs = new Dictionary<GameId, IConfig>();
        }

        public void OnServiceStart()
        {
            Resources.LoadAll<AbstractConfig>(string.Empty).ForEach(cfg => _configs.Add(cfg.Id, cfg));
        }

        public T GetConfig<T>(GameId configId) where T : class, IConfig =>
            _configs[configId] as T;

        public bool IsConfigExists<T>(GameId configId) where T : class, IConfig =>
            _configs.TryGetValue(configId, out var cfg) && cfg is T;

        public bool TryGetConfig<T>(GameId configId, out T config) where T : class, IConfig
        {
            config = default(T);

            if (!_configs.TryGetValue(configId, out var cfg)
                || cfg is not T typedCfg)
            {
                return false;
            }

            config = typedCfg;
            return true;
        }
    }
}