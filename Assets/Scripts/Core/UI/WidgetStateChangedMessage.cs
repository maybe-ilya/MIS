namespace mis.Core
{
    public readonly struct WidgetStateChangedMessage : IMessage
    {
        public readonly IWidget Widget;

        public WidgetStateChangedMessage(IWidget widget)
        {
            Widget = widget;
        }
    }
}