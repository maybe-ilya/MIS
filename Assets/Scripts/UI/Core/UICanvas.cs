using mis.Core;
using UnityEngine;

namespace mis.UI
{
    internal sealed class UICanvas : AbstractGameEntityComponent, IUICanvas
    {
        [SerializeField]
        [CheckObject]
        private RectTransform _hudContainer;

        [SerializeField]
        [CheckObject]
        private RectTransform _windowsContainer;

        public RectTransform HudContainer => _hudContainer;

        public RectTransform WindowsContainer => _windowsContainer;
    }
}