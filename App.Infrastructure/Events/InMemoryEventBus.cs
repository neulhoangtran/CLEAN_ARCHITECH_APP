using App.Domain.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace App.Infrastructure.Events
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly ConcurrentDictionary<Type, List<Type>> _handlers;
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _handlers = new ConcurrentDictionary<Type, List<Type>>();
            _serviceProvider = serviceProvider;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class
        {
            var eventType = @event.GetType();
            if (_handlers.TryGetValue(eventType, out var handlerTypes))
            {
                foreach (var handlerType in handlerTypes)
                {
                    var handlerInstance = _serviceProvider.GetService(handlerType) as IEventHandler<TEvent>;
                    if (handlerInstance != null)
                    {
                        handlerInstance.Handle(@event).Wait();
                    }
                }
            }
        }

        public void Subscribe<TEvent, THandler>()
            where TEvent : class
            where THandler : IEventHandler<TEvent>
        {
            var eventType = typeof(TEvent);
            var handlerType = typeof(THandler);

            if (!_handlers.ContainsKey(eventType))
            {
                _handlers[eventType] = new List<Type>();
            }

            _handlers[eventType].Add(handlerType);
        }

        public void Unsubscribe<TEvent, THandler>()
            where TEvent : class
            where THandler : IEventHandler<TEvent>
        {
            var eventType = typeof(TEvent);
            var handlerType = typeof(THandler);

            if (_handlers.ContainsKey(eventType))
            {
                _handlers[eventType].Remove(handlerType);
            }
        }
    }
}
