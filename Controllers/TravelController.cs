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
        // POST: api/Travel/AddTravel
        [ResponseType(typeof(Travel))]
        public IHttpActionResult AddTravel(Travel travel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            travel.travelDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            if (travel.travelOwner != null)
            {
                var accountById = db.Account.FirstOrDefault(p => p.user_Id == travel.travelOwner);
                if (accountById.travelOwner != null)
                {
                    accountById.travelOwner += "," + travel.travelOwner.ToString();
                }
                else
                {
                    accountById.travelOwner = travel.travelOwner.ToString();
                }
            }

            db.Travel.Add(travel);
            db.SaveChanges();

            return CreatedAtRoute("ActionApi", new { id = travel.travel_Id }, travel);
        }
        [HttpPost]
        // POST api/Travel/EditTravel
        [ResponseType(typeof(Travel))]
        public IHttpActionResult EditTravel (Travel travel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var travelById = db.Travel.FirstOrDefault(p => p.travel_Id == travel.travel_Id);
            if (travel.DATA != null)
            {
                travelById.DATA = travel.DATA;
            }
            if (travel.travelTitle != null)
            {
                travelById.travelTitle = travel.travelTitle;
            }
            if (travel.travelOwner != null)
            {
                travelById.travelOwner = travel.travelOwner;
            }
            if (travel.travelMap != null)
            {
                travelById.travelMap = travel.travelMap;
            }
            if (travel.travelInfo != null)
            {
                travelById.travelInfo = travel.travelInfo;
            }
            if (travel.travelCity != null)
            {
                travelById.travelCity = travel.travelCity;
            }
            if (travel.travelType != null)
            {
                travelById.travelType = travel.travelType;
            }
            if (travel.travelComment != null)
            {
                travelById.travelComment = travel.travelComment;
            }
            if (travel.travelRate != null)
            {
                travelById.travelRate = travel.travelRate;
            }
            if (travel.attractionReferral != null)
            {
                travelById.attractionReferral = travel.attractionReferral;
            }

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