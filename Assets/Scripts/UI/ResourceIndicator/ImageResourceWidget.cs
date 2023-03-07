using mis.Core;
using UnityEngine;
using UnityEngine.UI;

namespace mis.UI
{
    internal sealed class ImageResourceWidget : AbstractResourceIndicator
    {
        [SerializeField]
        [CheckObject]
        private Image _resourceFiller;

        protected override void OnInit() =>
            UpdateView(ResourceContainer.GetResource(_resourceId).Value);

        protected override void OnResourceAdded(uint oldValue, uint newValue, uint delta) =>
            UpdateView(newValue);

        protected override void OnResourceRemoved(uint oldValue, uint newValue, uint delta) =>
            UpdateView(newValue);

        private void UpdateView(uint value) =>
            _resourceFiller.fillAmount = ResourceContainer.GetResourceRatio(_resourceId, value);
    }
}