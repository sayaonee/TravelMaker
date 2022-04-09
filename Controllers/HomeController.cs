using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
using TravelMaker.Models;
using System.IO;
using EntityFramework.Extensions;

namespace TravelMaker.Controllers
{
    public class HomeController : Controller
    {
        private TravelMakerEntities db = new TravelMakerEntities();
        //Data Source=res10;Initial Catalog=TravelMaker;Persist Security Info=True;User ID=travelmaker;Password=Aa2022032208;Application Name=EntityFramework
        //發佈用
        public ActionResult Index()
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            return View();
        }
        public ActionResult Welcome()
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            return View();
        }
        public ActionResult AccountCreate()
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            return View();
        }
        public ActionResult AccountIndex()
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            return View(db.Account);
        }
        public ActionResult AccountEdit(int? id)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            if (id == null)
                return RedirectToAction("AccountIndex");
            return View(db.Account.FirstOrDefault(p => p.user_Id == id));
        }
        [HttpPost]
        public ActionResult AccountEdit(Account account)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            var accountById = db.Account.FirstOrDefault(p => p.user_Id == account.user_Id);
            accountById.userAccount = account.userAccount;
            accountById.userPassword = account.userPassword;
            accountById.userPhone = account.userPhone;
            accountById.userAddress = account.userAddress;
            accountById.userEmail = account.userEmail;
            accountById.registerDate = account.registerDate;
            accountById.roleAdmin = account.roleAdmin;
            accountById.attractionOwner = account.attractionOwner;
            accountById.travelOwner = account.travelOwner;
            accountById.userFavorite = account.userFavorite;
            accountById.email_ID = account.email_ID;
            accountById.email_Approved = account.email_Approved;
            accountById.userTravel = account.userTravel;
            db.SaveChanges();
            return RedirectToAction("AccountIndex");
        }
        [HttpPost]
        public ActionResult AccountSave(Account account)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            db.Account.Add(account);
            db.SaveChanges();
            return RedirectToAction("AccountIndex");
        }
        public ActionResult AccountDelete(int? id)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            if (id == null)
                return RedirectToAction("AccountIndex");
            db.Account.Delete(p => p.user_Id == id);
            db.SaveChanges();
            return RedirectToAction("AccountIndex");
        }
        public ActionResult AttractionCreate()
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            return View();
        }
        public ActionResult AttractionIndex()
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            return View(db.Attraction);
        }
        public ActionResult AttractionEdit(int? id)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            if (id == null)
                return RedirectToAction("AttractionIndex");
            return View(db.Attraction.FirstOrDefault(p => p.attr_Id == id));
        }
        [HttpPost]
        public ActionResult AttractionEdit(Attraction attraction)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            var attractionById = db.Attraction.FirstOrDefault(p => p.attr_Id == attraction.attr_Id);
            attractionById.position = attraction.position;
            attractionById.attractionCover = attraction.attractionCover;
            attractionById.attractionPhone = attraction.attractionPhone;
            attractionById.attractionAddress = attraction.attractionAddress;
            attractionById.attractionCity = attraction.attractionCity;
            attractionById.attractionType = attraction.attractionType;
            attractionById.attractionInfo = attraction.attractionInfo;
            attractionById.attractionPrice = attraction.attractionPrice;
            attractionById.lastUpdate = attraction.lastUpdate;
            attractionById.attrName = attraction.attrName;
            attractionById.attractionDistrict = attraction.attractionDistrict;
            attractionById.attractionCount = attraction.attractionCount;
            attractionById.attractionOwner = attraction.attractionOwner;
            db.SaveChanges();
            return RedirectToAction("AttractionIndex");
        }
        public ActionResult AttractionDelete(int? id)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            if (id == null)
                return RedirectToAction("AttractionIndex");
            db.Attraction.Delete(p => p.attr_Id == id);
            db.SaveChanges();
            return RedirectToAction("AttractionIndex");
        }
        public ActionResult AttractionSave(Attraction attraction)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            db.Attraction.Add(attraction);
            db.SaveChanges();
            return RedirectToAction("AttractionIndex");
        }
        public ActionResult TravelIndex()
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            return View(db.Travel);
        }
        public ActionResult TravelEdit(int? id)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            if (id == null)
                return RedirectToAction("TravelIndex");
            return View(db.Travel.FirstOrDefault(p => p.travel_Id == id));
        }
        [HttpPost]
        public ActionResult TravelEdit(Travel travel)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            var travelById = db.Travel.FirstOrDefault(p => p.travel_Id == travel.travel_Id);
            travelById.DATA = travel.DATA;
            travelById.travelTitle = travel.travelTitle;
            travelById.travelOwner = travel.travelOwner;
            travelById.travelMap = travel.travelMap;
            travelById.travelDate = travel.travelDate;
            travelById.travelCity = travel.travelCity;
            travelById.travelType = travel.travelType;
            travelById.attractionReferral = travel.attractionReferral;

            db.SaveChanges();
            return RedirectToAction("TravelIndex");
        }
        public ActionResult TravelDelete(int? id)
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            if (id == null)
                return RedirectToAction("TravelIndex");
            db.Travel.Delete(p => p.travel_Id == id);
            db.SaveChanges();
            return RedirectToAction("TravelIndex");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel vModel)
        {
            bool userCheck = false;
            string targetAccount = null;
            var accounts = db.Account;
            foreach (var p in accounts)
            {
                if (vModel.userAccount == p.userAccount && vModel.userPassword == p.userPassword)
                {
                    userCheck = true;
                    targetAccount = vModel.userAccount;
                }
            }
            if (userCheck)
            {
                Session[userDictionary.TM_LOGIN_USER] = targetAccount;
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewBag.AccountMsg = "登入失敗";
                return View();
            }
        }
        public ActionResult MapCenter()
        {
            var MCdata = db.MapCenter;
            return Json(MCdata, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Active(string activeToken)
        {
            string accountsActived = "";
            var accountsActive = db.Account.Select(p => new
            {
                p.userAccount,
                p.email_ID
            });
            foreach (var p in accountsActive)
            {
                if (activeToken == p.email_ID.ToString())
                {
                    accountsActived = p.userAccount;
                }
            }
            if (accountsActived == "")
            {
                ViewBag.Actvie = "未存在的驗證碼";
            }
            else
            {
                var accountByAccount = db.Account.FirstOrDefault(p => p.userAccount == accountsActived);
                accountByAccount.email_Approved = "Y";
                db.SaveChanges();
                ViewBag.Actvie = "帳號驗證成功!";
            }
            return View();
        }
        public ActionResult Image(string imgName)
        {
            var dir = Server.MapPath("~/Image");
            var path = Path.Combine(dir, imgName);
            return base.File(path, "image/jpg");
        }
    }
}