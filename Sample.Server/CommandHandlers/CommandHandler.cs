using Sample.Bus;

namespace Sample.Server.CommandHandlers
{
    public abstract class CommandHandler<T> : ICommandHandler<IMessage>
    {
        public void Handle(IMessage message)
        {
            if(message is T)
                Handle((T)message);
        }

        public abstract void Handle(T command);

        public void Start()
        {
        }
    }
}