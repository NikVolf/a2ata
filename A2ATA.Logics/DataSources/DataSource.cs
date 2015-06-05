using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.DataSources
{
    public abstract class DataSource : IContractResolver, IOfferResolver, IActorResolver
    {
        public abstract IEnumerable<Contracts.Contract> EnumerateContracts();

        public abstract IEnumerable<Contracts.Offers.Offer> EnumerateOffers();

        public abstract IEnumerable<Money.Actor> EnumerateActors();

        public virtual Contracts.Contract ResolveContract(string id)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(id));
            return this.EnumerateContracts().FirstOrDefault(x => x.Id == id);
        }

        public virtual Contracts.Offers.Offer ResolveOffer(string id)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(id));
            return this.EnumerateOffers().FirstOrDefault(x => x.Id == id);
        }

        public virtual Money.Actor ResolveActor(string id)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(id));
            return this.EnumerateActors().FirstOrDefault(x => x.Id == id);
            
        }

    }
}
