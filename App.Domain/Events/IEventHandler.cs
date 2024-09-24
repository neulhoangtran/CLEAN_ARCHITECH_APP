namespace App.Domain.Events
{
    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event);
    }
}
