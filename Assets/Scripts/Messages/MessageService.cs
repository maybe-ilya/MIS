using System;
using mis.Core;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace mis.Messages
{
    public sealed class MessageService : IMessageService
    {
        private readonly Dictionary<Type, Action<IMessage>> _actions;
        private readonly Dictionary<Delegate, Action<IMessage>> _handlers;

        public MessageService()
        {
            _actions = new Dictionary<Type, Action<IMessage>>();
            _handlers = new Dictionary<Delegate, Action<IMessage>>();
        }

        public void Subscribe<T>(Action<T> action) where T : IMessage
        {
            if (_handlers.ContainsKey(action))
            {
                return;
            }

            var newAction = _handlers[action] = (e) => action((T)e);

            var type = typeof(T);
            if (_actions.TryGetValue(type, out var foundAction))
            {
                _actions[type] = foundAction += newAction;
            }
            else
            {
                _actions[type] = newAction;
            }
        }

        public void Unsubscribe<T>(Action<T> action) where T : IMessage
        {
            if (!_handlers.TryGetValue(action, out var existingHandler))
            {
                return;
            }

            var type = typeof(T);
            if (_actions.TryGetValue(type, out var existingAction))
            {
                existingAction -= existingHandler;
                if (existingAction == null)
                {
                    _actions.Remove(type);
                }
                else
                {
                    _actions[type] = existingAction;
                }
            }

            _handlers.Remove(action);
        }

        public void Send<T>(T message) where T : IMessage
        {
            if (_actions.TryGetValue(message.GetType(), out var action))
            {
                action.Invoke(message);
            }
        }

        public void Send<T>() where T : IMessage, new() =>
            Send(new T());
    }
}