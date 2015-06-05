using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.DataSources
{
    public interface IContractResolver
    {
        IEnumerable<Contracts.Contract> EnumerateContracts();

        Contracts.Contract ResolveContract(string id);
    }
}
