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
    public class TravelController : ApiController
    {
        private TravelMakerEntities db = new TravelMakerEntities();

        // GET: api/Travel
        [HttpGet]
        public IQueryable GetTravel()
        {
            var travels = db.Travel;
            return travels;
        }
        // GET: api/Travel/mapcenter
        [HttpGet]
        public IQueryable MapCenter()
        {
            var travels = db.MapCenter;
            return travels;
        }
        // GET: api/Travel/5
        [HttpGet]
        [ResponseType(typeof(Travel))]
        public IHttpActionResult GetTravel(int id)
        {
            Travel travel = db.Travel.Find(id);
            if (travel == null)
            {
                return NotFound();
            }

            return Ok(travel);
        }
        // POST: api/Travel/PostTravel
        [ResponseType(typeof(Travel))]
        public IHttpActionResult PostTravel(Travel travel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            travel.travelDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            db.Travel.Add(travel);
            db.SaveChanges();

            return CreatedAtRoute("ActionApi", new { id = travel.travel_Id }, travel);
        }

        // DELETE: api/Travels/5
        [ResponseType(typeof(Travel))]
        public IHttpActionResult DeleteTravel(int id)
        {
            Travel travel = db.Travel.Find(id);
            if (travel == null)
            {
                return NotFound();
            }

            db.Travel.Remove(travel);
            db.SaveChanges();

            return Ok(travel);
        }
    }
}