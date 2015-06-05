using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A2ATA.Logics.Money;

namespace A2ATA.Logics.Factory.Bitcoin
{
    [CurrencyResolverPrefix(BitcoinCurrency.WalletQualifier)]
    public class BitcoinWalletAccount : Account
    {

        public BitcoinWalletAccount(string id)
        {
            this.CurrencyId = BitcoinCurrency.GlobalId;
            this.Id = id;
        }

    }
}
