namespace App.Domain.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : class; // Đảm bảo phương thức Publish trả về Task
        void Subscribe<TEvent, THandler>()
            where TEvent : class
            where THandler : IEventHandler<TEvent>;

        void Unsubscribe<TEvent, THandler>()
            where TEvent : class
            where THandler : IEventHandler<TEvent>;
    }
}
