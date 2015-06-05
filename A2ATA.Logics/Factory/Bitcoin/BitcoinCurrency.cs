using A2ATA.Logics.Money;

namespace A2ATA.Logics.Factory.Bitcoin
{
    public class BitcoinCurrency : Currency
    {
        public const string GlobalId = "bitcoin";

        public const string WalletQualifier = GlobalId + Resolver.Separator + "wallet";

        public const string AnyAccountId = 
            WalletQualifier + Resolver.Separator + 
            Resolver.AnyQualifier;

        public BitcoinCurrency()
        {
            this.Id = GlobalId;
            this.DisplayName = "Bitcoin";
        }
    }
}
