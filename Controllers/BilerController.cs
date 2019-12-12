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

        [HttpGet]
        [Route("count")]
        public int Count()
        {
            return mngBiler.CountBiler() ;
        }

        //// GET: api/Biler/5
        //[HttpGet("{id}", Name = "Get")]
        //public Bil GetFromID(int id)
        //{
        //    return mngBiler.GetBilFromId(id);
        //}

        [HttpGet]
        [Route("search")] //Bestemmer routen
        public IEnumerable<Bil> Search([FromQuery] QueryCar qcar)
        {
            return mngBiler.SearchBilerBegge(qcar);
        }

        [HttpGet]
        [Route("searchtid")] //Bestemmer routen
        public IEnumerable<Bil> SearchTid([FromQuery] QueryCar qcar)
        {
            return mngBiler.SearchBilerTid(qcar);
        }

        // POST: api/Biler
        [HttpPost]
        public void Post(Bil value)
        {
            mngBiler.CreateBil(new Bil());
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
