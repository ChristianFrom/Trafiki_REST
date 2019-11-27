using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trafiki_ModelLib;

namespace Trafiki_REST.Controllers
{
    [Route("api/biler")]
    [ApiController]
    public class BilerController : ControllerBase
    {
        public static List<Bil> data = new List<Bil>
        {
            new Bil(DateTime.Now, 1),
        };

        // GET: api/Biler
        [HttpGet]
        public IEnumerable<Bil> Get()
        {
            return data;
        }

        // GET: api/Biler/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Biler
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Biler/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
