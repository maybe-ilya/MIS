using UnityEngine;

namespace mis.Core
{
    public interface IUICanvas : IGameEntityComponent
    {
        RectTransform HudContainer { get; }

        RectTransform WindowsContainer { get; }
    }
}