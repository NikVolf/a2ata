using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.Contracts.Offers
{
    public class Party
    {
        public string Reference { get; set; }
        public Clause[] Clauses { get; set; }
    }
}
