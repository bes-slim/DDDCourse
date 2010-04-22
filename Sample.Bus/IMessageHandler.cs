namespace Sample.Bus
{
    public interface IMessageHandler
    {
        void Handle(IMessage message);
    }
}