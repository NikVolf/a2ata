using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.Factory.Bitcoin
{
    [AccountIdResolver(BitcoinCurrency.AnyAccountId)]
    [CurrencyResolverPrefix(BitcoinCurrency.WalletQualifier)]
    public class BitcoinAnyWalletAccount : BitcoinWalletAccount
    {
        public BitcoinAnyWalletAccount() : base(BitcoinCurrency.AnyAccountId)
        {
        }
    }
}
