namespace Sample.Server
{
    public interface IListener<T>
    {
        void Handle(T message);
    }
}