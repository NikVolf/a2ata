using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using A2ATA.Logics.Money;

namespace A2ATA.Logics.Factory
{
    public static class Resolver
    {

        public const string Separator = ".";
        public const string AccountsQualifier = "accounts";
        public const string AnyQualifier = "any";

        private static readonly ConcurrentDictionary<string, Currency> Currencies =
            new ConcurrentDictionary<string, Currency>();

        private static readonly object Lock = new object();

        internal static IEnumerable<Type> GetTypes<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes()
                    .Where(x => typeof(T).IsAssignableFrom(x) &&
                                !x.IsAbstract));
        }

        internal static IDictionary<Type, IEnumerable<TAttribute>> GetTypes<T, TAttribute>()
        {
            var query = GetTypes<T>().Select(x => new {type = x, attrs = x.GetCustomAttributes(true).OfType<TAttribute>()});
            return query.Where(x => x.attrs.Any()).ToDictionary(x => x.type, x => x.attrs);
        }

        internal static IEnumerable<Type> GetTypes<T, TAttribute>(Func<TAttribute, bool> attributePredicate)
        {
            return GetTypes<T, TAttribute>().Where(x => x.Value.Any(attributePredicate)).Select(x => x.Key);
        }

        internal static Type GetFirstType<T, TAttribute>(Func<TAttribute, bool> attributePredicate)
        {
            return GetTypes<T, TAttribute>(attributePredicate).FirstOrDefault();
        }

        internal static T CreateType<T, TAttribute>(Func<TAttribute, bool> attributePredicate, params object[] constructorParams)
            where T: class
        {
            var type = GetFirstType<T, TAttribute>(attributePredicate);
            if (type == null)
                return null;

            return constructorParams == null || constructorParams.Length == 0
                ? (T) Activator.CreateInstance(type)
                : (T) Activator.CreateInstance(type, constructorParams);
        }

        public static Account ResolveAccount(string prefix, string id)
        {
            var idInstance = CreateType<Account, AccountIdResolverAttribute>(attribute => attribute.ExactId == id);
            if (idInstance != null)
                return idInstance;

            var prefixInstance = CreateType<Account, CurrencyResolverPrefixAttribute>(
                attribute => attribute.Prefix == prefix,
                id);

            return prefixInstance;
        }

        public static Currency ResolveCurrency(string id)
        {
            lock (Lock)
            {
                var currency = default(Currency);
                if (Currencies.TryGetValue(id, out currency))
                    return currency;

                foreach (var type in GetTypes<Money.Currency>() )
                {
                    var instance = (Currency)Activator.CreateInstance(type);
                    if (instance.Id != id) continue;

                    Currencies[id] = instance;
                    return instance;
                }

                return null;


            }
        }

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal sealed class CurrencyResolverPrefixAttribute : Attribute
    {
        public CurrencyResolverPrefixAttribute(string prefix)
        {
            this.Prefix = prefix;
        }

        public string Prefix { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal sealed class AccountIdResolverAttribute : Attribute
    {
        public AccountIdResolverAttribute(string exactId)
        {
            this.ExactId = exactId;
        }

        public string ExactId { get; private set; }
    }
}
