namespace Sample.Server.Denormalizer.EventHandlers
{
    public interface IEventHandler<T> : IListener<T>, IStartable
    {
        
    }
}