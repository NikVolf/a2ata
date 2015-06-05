using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using A2ATA.Logics.Factory;
using A2ATA.Logics.Money;

namespace A2ATA.Logics.DataSources
{
    public class Api
    {
        public Api(
            IContractRegistrator contractRegistrator, 
            IOfferRegistrator offerRegistrator, 
            IContractResolver contractResolver, 
            IOfferResolver offerResolver, 
            IActorResolver actorResolver)
        {
            ContractRegistrator = contractRegistrator;
            OfferRegistrator = offerRegistrator;
            ContractResolver = contractResolver;
            OfferResolver = offerResolver;
            ActorResolver = actorResolver;
        }

        internal IContractResolver ContractResolver { get; private set; }
        internal IOfferResolver OfferResolver { get; private set; }
        internal IActorResolver ActorResolver { get; private set; }

        internal IContractRegistrator ContractRegistrator { get; private set; }

        internal IOfferRegistrator OfferRegistrator { get; private set; }

        // ReSharper disable once InconsistentNaming
        public Account account(string id)
        {
            //System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(id));

            var parts = id.Split(new[] {Resolver.Separator}, StringSplitOptions.None);
            if (parts.Length < 2)
                throw Exception("Invalid account qualifier '{0}'", id);

            var prefix = string.Join(Resolver.Separator, parts.Take(parts.Length - 1));
            return Resolver.ResolveAccount(prefix, id);
        }

        private static InvalidOperationException Exception(string message, params object[] args)
        {
            return new InvalidOperationException(string.Format(message, args));
        }

    }
}
