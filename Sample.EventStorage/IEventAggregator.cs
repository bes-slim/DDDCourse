using System.Collections.Generic;

namespace Sample.EventStorage
{
    public interface IEventAggregator
    {
        void SendMessage<T>(T message);
        void SendMessage<T>() where T : new();
        void AddListener(object listener);
        void RemoveListener(object listener);
        void SendMessages<T>(IEnumerable<T> messages);
    }
}