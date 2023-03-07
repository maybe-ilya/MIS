using mis.Core;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace mis.UI
{
    internal abstract class WidgetController<T> where T : IWidget
    {
        private readonly RectTransform _root;
        private readonly IObjectService _objectService;
        private readonly IMessageService _messageService;
        private readonly List<T> _widgets;

        public WidgetController(
            RectTransform root,
            IObjectService objectService,
            IMessageService messageService)
        {
            _root = root;
            _objectService = objectService;
            _widgets = new List<T>();
            _messageService = messageService;

            _messageService.Subscribe<WidgetStateChangedMessage>(OnWidgetStateChanged);
        }

        public T Open(GameId windowTypeId)
        {
            HideAll();

            var newWidget = _objectService.SpawnEntityByType<T>(windowTypeId, parent: _root);
            _widgets.Insert(0, newWidget);
            newWidget.Open();
            return newWidget;
        }

        public void Show(GameId windowTypeId)
        {
            var window = _widgets.FirstOrDefault(window => window.Entity.TypeId == windowTypeId);
            if (window != null)
            {
                window.Show();
            }
        }

        public void Show(uint id)
        {
            var window = _widgets.FirstOrDefault(window => window.Entity.Id == id);
            if (window != null)
            {
                window.Show();
            }
        }

        public void Hide(GameId windowTypeId)
        {
            var window = _widgets.FirstOrDefault(window => window.Entity.TypeId == windowTypeId);
            if (window != null)
            {
                window.Hide();
            }
        }

        public void Hide(uint id)
        {
            var window = _widgets.FirstOrDefault(window => window.Entity.Id == id);
            if (window != null)
            {
                window.Hide();
            }
        }

        public void Close(GameId windowTypeId)
        {
            var window = _widgets.FirstOrDefault(window => window.Entity.TypeId == windowTypeId);
            if (window != null)
            {
                window.Close();
            }
        }

        protected void RemoveWidget(T widget)
        {
            if (widget == null)
            {
                return;
            }

            _widgets.Remove(widget);
            _objectService.DespawnEntity(widget);
        }

        protected void TryShowWidgetIfThereIsNone()
        {
            if (_widgets.Count > 0 && !_widgets.Any(window => window.State == WidgetState.Shown))
            {
                _widgets[0].Show();
            }
        }

        protected void HideAll()
        {
            foreach (var widget in _widgets)
            {
                widget.Hide();
            }
        }

        public void CloseAll()
        {
            for (var index = _widgets.Count - 1; index >= 0; --index)
            {
                _widgets[index].Close();
            }
        }

        protected virtual void OnWidgetStateChanged(WidgetStateChangedMessage message) { }
    }
}