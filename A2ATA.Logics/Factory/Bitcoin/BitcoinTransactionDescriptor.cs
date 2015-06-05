using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A2ATA.Logics.Money;

namespace A2ATA.Logics.Factory.Bitcoin
{
    public class BitcoinTransactionDescriptor : TransactionDescriptor
    {
        public BitcoinTransactionDescriptor(string id)
        {
            this.Id = id;
        }
    }
}
