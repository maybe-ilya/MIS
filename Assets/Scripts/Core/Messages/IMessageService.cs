using System;

namespace mis.Core
{
    public interface IMessageService : IService
    {
        void Subscribe<T>(Action<T> action) where T : IMessage;
        void Unsubscribe<T>(Action<T> action) where T : IMessage;
        void Send<T>(T message) where T : IMessage;
        void Send<T>() where T : IMessage, new();
    }
}