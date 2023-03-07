using mis.Core;
using TMPro;
using UnityEngine;

namespace mis.UI
{
    internal sealed class TextResourceWidget : AbstractResourceIndicator
    {
        [SerializeField]
        [CheckObject]
        private TMP_Text _resourceText;

        protected override void OnInit() =>
            UpdateText(ResourceContainer.GetResource(_resourceId).Value);

        protected override void OnResourceAdded(uint oldValue, uint newValue, uint delta) =>
            UpdateText(newValue);

        protected override void OnResourceRemoved(uint oldValue, uint newValue, uint delta) =>
            UpdateText(newValue);

        private void UpdateText(uint value) =>
            _resourceText.text = value.ToString();
    }
}