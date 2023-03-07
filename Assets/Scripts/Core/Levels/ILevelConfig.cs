using UnityEngine;

namespace mis.Core
{
    public interface ILevelConfig : IConfig
    {
        string SceneName { get; }
        GameId PlayerPawnId { get; }
        GameId HUDId { get; }
    }
}