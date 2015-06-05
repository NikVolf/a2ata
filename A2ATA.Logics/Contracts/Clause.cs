using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.Contracts
{
    public class Clause
    {
        public string SubjectActorId { get; set; }

        public string AccountId { get; set; }

        public decimal Amount { get; set; }

        public TimeSpan TimeLimit { get; set; }
    }
}
