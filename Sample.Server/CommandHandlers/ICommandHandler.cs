namespace Sample.Server.CommandHandlers
{
    public interface ICommandHandler<T> : IListener<T>, IStartable
    {
        
    }
}