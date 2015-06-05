using System;

namespace A2ATA.Logics.Contracts.Offers
{
    public class Clause
    {

        public string AccountId { get; set; }

        public decimal Amount { get; set; }

        public TimeSpan TimeLimit { get; set; }

        public string Text { get; set; }
    }
}
