using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Domain;
using Sample.Events;
using Sample.EventStorage;

namespace Sample.Specs
{
    [When]
    public class active_inventory_item_is_deactivated : AggregateRootTestFixture<InventoryItem>
    {
        protected override IEnumerable<IEvent> Given()
        {
            yield return New.InventoryItemActivatedEvent;
        }

        protected override void When()
        {
            Sut.Deactivate();
        }

        [Then]
        public void it_publishes_a_deactivated_event()
        {
            Events.First().ShouldBeInstanceOfType<InventoryItemDeactivatedEvent>();
        }

        [Then]
        public void the_published_event_holds_the_id_of_the_aggregate_root()
        {
            Events.First().As<InventoryItemDeactivatedEvent>().AggregateId.ShouldBe(Sut.Id);
        }
    }

    [When]
    public class inactive_inventory_item_is_activated : AggregateRootTestFixture<InventoryItem>
    {
        protected override IEnumerable<IEvent> Given()
        {
            yield return New.InventoryItemDeactivatedEvent;
        }

        protected override void When()
        {
            Sut.Activate();
        }

        [Then]
        public void it_publishes_an_activated_event()
        {
            Events.First().ShouldBeInstanceOfType<InventoryItemActivatedEvent>();
        }

        [Then]
        public void the_published_event_holds_the_id_of_the_aggregate_root()
        {
            Events.First().As<InventoryItemActivatedEvent>().AggregateId.ShouldBe(Sut.Id);
        }
    }

    [When]
    public class inactive_inventory_item_is_deactivated : AggregateRootTestFixture<InventoryItem>
    {
        protected override IEnumerable<IEvent> Given()
        {
            yield return New.InventoryItemDeactivatedEvent;
        }

        protected override void When()
        {
            Sut.Deactivate();
        }

        [Then]
        public void it_throws_an_invalid_operation_exception()
        {
            Caught.ShouldBeInstanceOfType<InvalidOperationException>();
        }
    }

    [When]
    public class active_inventory_item_is_activated : AggregateRootTestFixture<InventoryItem>
    {
        protected override IEnumerable<IEvent> Given()
        {
            yield return New.InventoryItemActivatedEvent;
        }

        protected override void When()
        {
            Sut.Activate();
        }

        [Then]
        public void it_throws_an_invalid_operation_exception()
        {
            Caught.ShouldBeInstanceOfType<InvalidOperationException>();
        }
    }

    [When]
    public class inventory_item_count_is_adjusted : AggregateRootTestFixture<InventoryItem>
    {
        const int AdjustedBy = 10;

        protected override IEnumerable<IEvent> Given()
        {
            return No.Changes;
        }

        protected override void When()
        {
            Sut.AdjustInventoryCount(AdjustedBy);
        }

        [Then]
        public void it_publishes_a_count_adjusted_event()
        {
            Events.First().ShouldBeInstanceOfType<InventoryItemCountAdjustedEvent>();
        }

        [Then]
        public void the_published_event_holds_the_id_of_the_aggregate_root()
        {
            Events.First().As<InventoryItemCountAdjustedEvent>().AggregateId.ShouldBe(Sut.Id);
        }

        [Then]
        public void the_published_event_holds_the_adjusted_count()
        {
            Events.First().As<InventoryItemCountAdjustedEvent>().AdjustedBy.ShouldBe(AdjustedBy);
        }
    }
}