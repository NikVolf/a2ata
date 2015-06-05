using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.DataSources
{
    public interface IOfferResolver
    {
        IEnumerable<Contracts.Offers.Offer> EnumerateOffers();

        Contracts.Offers.Offer ResolveOffer(string id);
    }
}
