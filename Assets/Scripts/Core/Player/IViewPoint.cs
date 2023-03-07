using UnityEngine;

namespace mis.Core
{
    public interface IViewPoint
    {
        Vector3 Position { get; }
        Vector3 ForwardDirection { get; }
        Vector3 UpDirection { get; }
        Vector3 RightDirection { get; }
    }
}