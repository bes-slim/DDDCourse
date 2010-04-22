using Sample.EventStorage;

namespace Sample.Server.Denormalizer.EventHandlers
{
    public abstract class EventHandler<T> : IEventHandler<IEvent>
    {
        public void Handle(IEvent message)
        {
            if(message is T)
                Handle((T)message);
        }

        public abstract void Handle(T @event);

        public void Start()
        {
        }
    }
}