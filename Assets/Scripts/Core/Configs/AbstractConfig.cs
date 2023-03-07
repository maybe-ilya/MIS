using UnityEngine;

namespace mis.Core
{
    public abstract class AbstractConfig : ScriptableObject, IConfig
    {
        [SerializeField]
        private GameId _id;

        public GameId Id => _id;
    }
}