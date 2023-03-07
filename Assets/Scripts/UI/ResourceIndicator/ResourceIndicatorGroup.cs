using mis.Core;
using UnityEngine;

namespace mis.UI
{
    internal sealed class ResourceIndicatorGroup : MonoBehaviour
    {
        [SerializeField]
        [CheckObject]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        [CheckObject]
        private AbstractResourceIndicator[] _indicators;

        public bool IsVisible => _canvasGroup.alpha > 0.0f;

        public void Init(BaseResourceContainer resourceContainer)
        {
            _indicators.ForEach(i => i.Init(resourceContainer));
        }

        public void Deinit()
        {
            _indicators.ForEach(i => i.Deinit());
        }

        public void SetResourceId(GameId resourceId)
        {
            _indicators.ForEach(i => i.SetResourceId(resourceId));
        }

        public void SetVisible(bool visible)
        {
            _canvasGroup.alpha = visible ? 1 : 0;
        }
    }
}