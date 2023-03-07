using mis.Core;
using UnityEngine;

namespace mis.Weapons
{
    public abstract class AbstractProjectile : AbstractGameEntityComponent, IProjectile
    {
        public abstract void Setup(Vector3 origin, Vector3 target);
    }
}