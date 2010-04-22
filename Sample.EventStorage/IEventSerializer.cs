namespace Sample.EventStorage
{
    public interface IEventSerializer
    {
        byte[] Serialize(IEvent change);
        IEvent Deserialize(byte[] bytes);
    }
}