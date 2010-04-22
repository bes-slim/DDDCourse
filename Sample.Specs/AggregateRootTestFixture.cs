using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sample.Domain;
using Sample.EventStorage;

namespace Sample.Specs
{
    public abstract class AggregateRootTestFixture<T> where T : IAggregateRoot
    {
        protected Exception Caught;
        protected T Sut;
        protected IEnumerable<IEvent> Events;

        protected abstract IEnumerable<IEvent> Given();
        protected abstract void When();

        [SetUp]
        public void Setup()
        {
            Caught = null;
            Sut = (T)Activator.CreateInstance(typeof(T), Guid.NewGuid());
            Sut.LoadFromHistory(Given());

            try
            {
                When();
                Events = Sut.GetChanges();
            }
            catch (Exception e)
            {
                Caught = e;
            }
        }
    }
}
