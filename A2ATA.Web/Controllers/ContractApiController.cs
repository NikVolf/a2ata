using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace A2ATA.Web.Controllers
{
    public class ContractApiController : ApiController
    {
        // GET api/contractapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/contractapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/contractapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/contractapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/contractapi/5
        public void Delete(int id)
        {
        }
    }
}
