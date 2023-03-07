using UnityEngine;
using mis.Core;

namespace mis.UI
{
    internal abstract class AbstractWidget : AbstractGameEntityComponent, IWidget
    {
        [SerializeField]
        [CheckObject]
        private CanvasGroup _canvasGroup;

        public WidgetState State { get; private set; }

        protected IMessageService MessageService { get; private set; }

        public void Open()
        {
            if (State == WidgetState.Opened)
            {
                return;
            }

            Initialize();
            SetState(WidgetState.Opened);
            OnOpened();
            Show();
        }

        public void Show()
        {
            if (State == WidgetState.Shown)
            {
                return;
            }

            _canvasGroup.alpha = 1;
            SetState(WidgetState.Shown);
            OnShown();
        }

        public void Hide()
        {
            if (State == WidgetState.Hidden)
            {
                return;
            }

            _canvasGroup.alpha = 0;
            SetState(WidgetState.Hidden);
            OnHidden();
        }

        public void Close()
        {
            if (State == WidgetState.Closed)
            {
                return;
            }

            SetState(WidgetState.Closed);
            OnClosed();
            Deinitialize();
        }

        protected void SetState(WidgetState state)
        {
            State = state;
            MessageService.Send(new WidgetStateChangedMessage(this));
        }

        protected virtual void OnOpened() { }

        protected virtual void OnShown() { }

        protected virtual void OnHidden() { }

        protected virtual void OnClosed() { }

        private void Initialize()
        {
            MessageService = GameServices.Get<IMessageService>();
        }

        private void Deinitialize()
        {
            MessageService = null;
        }
    }
}