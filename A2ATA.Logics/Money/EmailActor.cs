using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ATA.Logics.Money
{
    public class EmailActor : Actor
    {
        public EmailActor(string email)
        {
            this.Id = email;
        }
    }
}
