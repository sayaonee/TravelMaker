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
            int countValue = 0;
            if (userDictionary.TM_ATTRACTION_COUNT.TryGetValue(id, out countValue))
            {
                userDictionary.TM_ATTRACTION_COUNT[id] += 1;
            }
            else
            {
                userDictionary.TM_ATTRACTION_COUNT.Add(id, 1);
            }
            return Ok(attractions);
        }
        [HttpGet]
        // GET:api/Attraction/GetAttractionCount
        public IHttpActionResult GetAttractionCount()
        {
            foreach (KeyValuePair<int, int> item in userDictionary.TM_ATTRACTION_COUNT)
            {
                var attractionById = db.Attraction.FirstOrDefault(p => p.attr_Id == item.Key);
                attractionById.attractionCount = item.Value;
            };
            db.SaveChanges();
            return Ok(userDictionary.TM_ATTRACTION_COUNT);
        }
        [HttpPost]
        // POST api/Attraction/PostAttractionDetail
        [ResponseType(typeof(Attraction))]
        public IHttpActionResult PostAttractionDetail(Attraction attraction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var attractionById = db.Attraction.FirstOrDefault(p => p.attr_Id == attraction.attr_Id);
            if (attraction.attractionPhone != null)
            {
                attractionById.attractionPhone = attraction.attractionPhone;
            }
            if (attraction.attractionPrice != null)
            {
                attractionById.attractionPrice = attraction.attractionPrice;
            }
            if (attraction.attrName != null)
            {
                attractionById.attrName = attraction.attrName;
            }
            if (attraction.attractionAddress != null)
            {
                attractionById.attractionAddress = attraction.attractionAddress;
            }
            if (attraction.attractionType != null)
            {
                attractionById.attractionType = attraction.attractionType;

            }
            if (attraction.attractionImages != null)
            {
                attractionById.attractionImages = attraction.attractionImages;
            }
            if (attraction.attractionCover != null)
            {
                attractionById.attractionCover = attraction.attractionCover;
            }

            db.SaveChanges();
            return CreatedAtRoute("ActionApi", new { id = attraction.attr_Id }, attraction);
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
            var hotAttraction = db.Attraction.OrderByDescending(p => p.attractionCount);
            int dbCount = db.Attraction.Count() + 1;
            int hotCount = 0;
            Random rnd = new Random();
            foreach(var p in hotAttraction)
            {
                hotActiveL.Add(db.Attraction.Find(p.attr_Id));
                duplicated.Add(p.attr_Id);
                hotCount += 1;
                if (hotCount == 5)
                {
                    break;
                }
            }
            //for (int i = 0; i < 8; i++)
            //{
            //    int rndId = rnd.Next(1, dbCount);
            //    if (duplicated.Contains(rndId))
            //    {
            //        i--;
            //    }
            //    else
            //    {
            //        Attraction rndAttraction = db.Attraction.Find(rndId);
            //        hotActiveL.Add(rndAttraction);
            //        duplicated.Add(rndId);
            //    }
            //}
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