using System;

namespace App.Domain.Events
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event) where TEvent : class;
        void Subscribe<TEvent, THandler>()
            where TEvent : class
            where THandler : IEventHandler<TEvent>;
        void Unsubscribe<TEvent, THandler>()
            where TEvent : class
            where THandler : IEventHandler<TEvent>;
    }
}
