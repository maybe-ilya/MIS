using mis.Core;
using System;
using UnityEngine.Pool;

namespace mis.Objects
{
    public class GameEntityPool : ObjectPool<GameEntity>
    {
        public GameEntityPool(
            Func<GameEntity> createFunc,
            Action<GameEntity> actionOnGet = null,
            Action<GameEntity> actionOnRelease = null,
            Action<GameEntity> actionOnDestroy = null,
            bool collectionCheck = true,
            int defaultCapacity = 10,
            int maxSize = 10000)
            : base(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize)
        {
        }
    }
}