using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.DataSources
{
    public interface IActorResolver
    {
        IEnumerable<Money.Actor> EnumerateActors();

        Money.Actor ResolveActor(string id);
    }
}
