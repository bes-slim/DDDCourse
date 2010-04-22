using System;

namespace Sample.Utilities
{
    public static class DelegateExtensions
    {
        public static Action<TBase> Cast<TBase, TDerived>(this Action<TDerived> source) where TDerived : TBase
        {
            return baseValue => source((TDerived)baseValue);
        }
    }
}