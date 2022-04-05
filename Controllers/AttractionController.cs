using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TravelMaker.Models;

namespace TravelMaker.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AttractionController : ApiController
    {
        private TravelMakerEntities db = new TravelMakerEntities();
        [HttpGet]
        [ResponseType(typeof(Attraction))]
        // GET: api/Attraction/GetAttractionDetail
        public IQueryable<Attraction> GetAttractionDetail()
        {
            return db.Attraction;
        }
        [HttpGet]
        // GET:api/Attraction/GetAttractionDetail/3 
        public IHttpActionResult GetAttractionDetail(int id)
        {
            Attraction attractions = db.Attraction.Find(id);
            if (attractions == null)
            {
                return NotFound();
            }
            return Ok(attractions);
        }
        [HttpGet]
        // GET:api/Attraction/homeAttraction
        public IHttpActionResult homeAttraction()
        {
            List<int> duplicated = new List<int>();
            string[] rainTypeTemp = { "藝術博物館", "博物館", "購物中心", "咖啡店", "百貨公司", "文化中心", "古蹟博物館", "科學技術館", "啤酒釀製廠", "餐廳景點", "藝術中心" };
            List<string> rainType = new List<string>(rainTypeTemp);
            string mystery = "旅遊景點";
            List<Attraction> hotActiveL = new List<Attraction>();
            List<Attraction> recommendL = new List<Attraction>();
            List<Attraction> rainL = new List<Attraction>();
            List<Attraction> mysteryL = new List<Attraction>();
            Dictionary<string, List<Attraction>> homeAttraction = new Dictionary<string, List<Attraction>>();
            int dbCount = db.Attraction.Count() + 1;
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                int rndId = rnd.Next(1, dbCount);
                if (duplicated.Contains(rndId))
                {
                    i--;
                }
                else
                {
                    Attraction rndAttraction = db.Attraction.Find(rndId);
                    hotActiveL.Add(rndAttraction);
                    duplicated.Add(rndId);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                int rndId = rnd.Next(1, dbCount);
                if (duplicated.Contains(rndId))
                {
                    i--;
                }
                else
                {
                    Attraction rndAttraction = db.Attraction.Find(rndId);
                    recommendL.Add(rndAttraction);
                    duplicated.Add(rndId);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                int rndId = rnd.Next(1, dbCount);
                Attraction dbTemp = db.Attraction.Find(rndId);
                if (duplicated.Contains(rndId) || rainTypeTemp.Contains(dbTemp.attractionType))
                {
                    i--;
                }
                else
                {
                    Attraction rndAttraction = db.Attraction.Find(rndId);
                    rainL.Add(rndAttraction);
                    duplicated.Add(rndId);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                int rndId = rnd.Next(1, dbCount);
                Attraction dbTemp = db.Attraction.Find(rndId);
                if (duplicated.Contains(rndId) || mystery != dbTemp.attractionType)
                {
                    i--;
                }
                else
                {
                    Attraction rndAttraction = db.Attraction.Find(rndId);
                    mysteryL.Add(rndAttraction);
                    duplicated.Add(rndId);
                }
            }
            homeAttraction.Add("hotActive", hotActiveL);
            homeAttraction.Add("recommend", recommendL);
            homeAttraction.Add("rain", rainL);
            homeAttraction.Add("mystery", mysteryL);
            return Ok(homeAttraction);
        }

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}