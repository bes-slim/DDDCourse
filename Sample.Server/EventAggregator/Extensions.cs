using System;
using System.Collections;
using System.Collections.Generic;
using StructureMap;

namespace Sample.Server.EventAggregator
{
    public static class Extensions
    {
        public static void Each<T>(this IList<T> list, Action<T> action)
        {
            if (list == null) return;

            foreach (T t in list)
            {
                action(t);
            }
        }

        public static void CallOnEach<T>(this IEnumerable enumerable, Action<T> action) where T : class
        {
            foreach (object o in enumerable)
            {
                o.CallOn(action);
            }
        }

        public static void CallOn<T>(this object target, Action<T> action) where T : class
        {
            var subject = target as T;
            if (subject != null)
            {
                try
                {
                    action(subject);
                }
                catch (InvalidOperationException e)
                {
                    if (!e.ToString().Contains("The calling thread"))
                    {
                        throw;
                    }
                }
            }
        }

        public static bool ImplementsInterfaceTemplate(this Type pluggedType, Type templateType)
        {
            if (pluggedType.IsConcrete())
            {
                foreach (Type type in pluggedType.GetInterfaces())
                {
                    if (type.IsGenericType && (type.GetGenericTypeDefinition() == templateType))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsConcrete(this Type type)
        {
            return !type.IsInterface && !type.IsAbstract;
        }
        
        public static bool Is<T>(this PluginTypeConfiguration configuration)
        {
            return typeof(T).IsAssignableFrom(configuration.PluginType);
        }

        public static T To<T>(this PluginTypeConfiguration configuration, IContainer container)
        {
            return (T)container.GetInstance(configuration.PluginType);
        }
    }
}