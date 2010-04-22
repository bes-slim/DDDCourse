using System.Threading;
using Sample.Commands;
using Sample.Events;
using Sample.EventStorage;
using Sample.Server.CommandHandlers;
using Sample.Server.Denormalizer.EventHandlers;
using Sample.Server.EventAggregator;
using StructureMap.Configuration.DSL;
using Sample.Domain;

namespace Sample.Server
{
    public class ServerRegistry : Registry
    {
        public ServerRegistry()
        {
            Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AssemblyContainingType<IEventStorage>();
                    x.WithDefaultConventions();
                });

            RegisterEventAggregator();
            SetupSynchronization();
            RegisterRepository();

            For<CommandHandler<ActivateInventoryItemCommand>>().Use<ActivateInventoryItemCommandHandler>();
            For<CommandHandler<AdjustInventoryCountCommand>>().Use<AdjustInventoryCountCommandHandler>();
            For<CommandHandler<CreateInventoryItemCommand>>().Use<CreateInventoryItemCommandHandler>();
            For<CommandHandler<DeactivateInventoryItemCommand>>().Use<DeactivateInventoryItemCommandHandler>();
            For<CommandHandler<ChangeInventoryItemDetailsCommand>>().Use<ChangeInventoryItemDetailsCommandHandler>();
            
            For<EventHandler<InventoryItemActivatedEvent>>().Use<InventoryItemActivatedEventHandler>();
            For<EventHandler<InventoryItemDeactivatedEvent>>().Use<InventoryItemDeactivatedEventHandler>();
            For<EventHandler<InventoryItemCountAdjustedEvent>>().Use<InventoryItemCountAdjustedEventHandler>();
            For<EventHandler<InventoryItemCreatedEvent>>().Use<InventoryItemCreatedEventHandler>();
            For<EventHandler<InventoryItemDetailsChangedEvent>>().Use<InventoryItemDetailsChangedEventHandler>();
        }

        void RegisterRepository()
        {
            For(typeof(IRepository<>)).Use(typeof(Repository<>));
        }

        void RegisterEventAggregator()
        {
            For<IEventAggregator>().AsSingletons().Use<EventAggregatorImpl>();
            RegisterInterceptor(new EventAggregatorInterceptor());
        }

        void SetupSynchronization()
        {
            ForSingletonOf<SynchronizationContext>().TheDefault.Is.ConstructedBy(() =>
            {
                if (SynchronizationContext.Current == null)
                {
                    SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
                }

                return SynchronizationContext.Current;
            });
        }
    }
}