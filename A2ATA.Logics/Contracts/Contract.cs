using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.Contracts
{
    public class Contract
    {
        public string Id { get; set; }

        public Clause[] Clauses { get; set; }

        public string ArbitratorActorId { get; set; }

        public string Text { get; set; }

    }
}
