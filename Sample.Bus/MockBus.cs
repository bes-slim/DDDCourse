using System.Collections.Generic;
using System.Diagnostics;
using Sample.Utilities;

namespace Sample.Bus
{
    public class MockBus : IBus
    {
        public void Publish(IMessage command)
        {
            Debug.Print(command.GetType().Name);
        }

        public void Publish(IEnumerable<IMessage> commands)
        {
            commands.ForEach(Publish);
        }
    }
}