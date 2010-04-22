using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sample.EventStorage
{
    public class EventSerializer : IEventSerializer
    {
        public byte[] Serialize(IEvent change)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, change);
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }

        public IEvent Deserialize(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return (IEvent)new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}