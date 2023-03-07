using mis.Core;
using UnityEngine;

namespace mis.UI
{
    internal class HUDController : WidgetController<IHUD>
    {
        public HUDController(
            RectTransform root,
            IObjectService objectService,
            IMessageService messageService)
            : base(root, objectService, messageService)
        {
        }

        protected override void OnWidgetStateChanged(WidgetStateChangedMessage message)
        {
            if (message.Widget.State != WidgetState.Closed
                || message.Widget is not IHUD hud)
            {
                return;
            }

            RemoveWidget(hud);
            TryShowWidgetIfThereIsNone();
        }
    }
}