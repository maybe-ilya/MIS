using mis.Core;

namespace mis.Messages
{
    public abstract class AbstractMessageHandler<T> : IMessageHandler where T : IMessage
    {
        protected readonly IMessageService _messageService;

        public AbstractMessageHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void Subscribe()
        {
            _messageService.Subscribe<T>(OnMessageInvoked);
        }

        protected abstract void OnMessageInvoked(T message);
    }
}