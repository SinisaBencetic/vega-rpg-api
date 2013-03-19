using System.Collections.Generic;
using System.Web.Http;

namespace VegaRpgWebApi.Controllers
{   
    public class ValuesController : ApiController
    {
        public class DummyWrapper
        {
            public DummyEntity dummy { get; set; }
            public class DummyEntity
            {
                public string value { get; set; }
            }
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public string Post(DummyWrapper dummy)
        {
            //return dummy.value[0].ToString();
            return "valuepost";
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}