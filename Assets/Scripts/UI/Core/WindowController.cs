using mis.Core;
using UnityEngine;

namespace mis.UI
{
    internal class WindowController : WidgetController<IWindow>
    {
        public WindowController(
            RectTransform root,
            IObjectService objectService,
            IMessageService messageService)
            : base(root, objectService, messageService)
        {
        }

        protected override void OnWidgetStateChanged(WidgetStateChangedMessage message)
        {
            if (message.Widget.State != WidgetState.Closed
                || message.Widget is not IWindow window)
            {
                return;
            }

            RemoveWidget(window);
            TryShowWidgetIfThereIsNone();
        }
    }
}