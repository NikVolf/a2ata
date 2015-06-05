using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.Money
{
    public class Performance
    {
        public string ActorId { get; set; }
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string ValidatorActorId { get; set; }
        public string TransactionDescriptorId { get; set; }
    }
}
