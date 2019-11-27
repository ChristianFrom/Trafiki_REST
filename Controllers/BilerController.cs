using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trafiki_ModelLib;
using Trafiki_REST.DBUtil;

namespace Trafiki_REST.Controllers
{
    [Route("api/biler")]
    [ApiController]
    public class BilerController : ControllerBase
    {
        ManageBiler mngBiler = new ManageBiler();
        
        // GET: api/Biler
        [HttpGet]
        public IEnumerable<Bil> Get()
        {

            return mngBiler.getAllBiler() ;
        }

        // GET: api/Biler/5
        [HttpGet("{id}", Name = "Get")]
        public Bil Get(int id)
        {
            return mngBiler.GetBilFromId(id);
        }

        // POST: api/Biler
        [HttpPost]
        public void Post([FromBody] Bil value)
        {
            mngBiler.CreateBil(value);
        }

        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            mngBiler.deleteBil(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void DeleteAllBiler()
        {
            mngBiler.deleteAllBiler();
        }
    }
}
