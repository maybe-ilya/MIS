using System.Collections.Generic;
using UnityEngine;

namespace mis.Core
{
    public interface ITetherAreaInfoProvider
    {
        IList<Vector3> GetVisiblePoints { get; }
        IList<Vector3> GetVisiblePointsInFrontOnPlayer { get; }
    }
}