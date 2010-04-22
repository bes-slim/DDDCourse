using System.Collections.Generic;
using System.Linq;
using Sample.Bus;
using Sample.EventStorage;
using Sample.Server.EventAggregator;
using StructureMap;

namespace Sample.Server
{
    public class Bootstrapper
    {
        public void Bootstrap()
        {
            IContainer container = BootstrapIoC();

            List<IStartable> startables = container.Model.PluginTypes
                .Where(p => p.Is<IStartable>())
                .Select(x => x.To<IStartable>(container)).ToList();

            startables.Each(x => x.Start());

            var commandRouter = new MsmqMessageReceiver(@".\Private$\Commands", new CommandMessageHandler(ObjectFactory.GetInstance<IEventAggregator>()));
            commandRouter.Start();
        }

        static IContainer BootstrapIoC()
        {
            ObjectFactory.Initialize(x => x.AddRegistry<ServerRegistry>());

            return ObjectFactory.Container;
        }
    }
}