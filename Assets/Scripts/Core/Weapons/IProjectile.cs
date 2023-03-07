using System;
using UnityEngine;

namespace mis.Core
{
    public interface IProjectile : IGameEntityComponent
    {
        void Setup(Vector3 origin, Vector3 target);
    }
}