using System;
using Sample.EventStorage;
using StructureMap;
using StructureMap.Interceptors;

namespace Sample.Server.EventAggregator
{
    public class EventAggregatorInterceptor : TypeInterceptor
    {
        public object Process(object target, IContext context)
        {
            context.GetInstance<IEventAggregator>().AddListener(target);
            return target;
        }

        public bool MatchesType(Type type)
        {
            return type.ImplementsInterfaceTemplate(typeof(IListener<>));
        }
    }
}