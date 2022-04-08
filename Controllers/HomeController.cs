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

namespace TravelMaker.Controllers
{
    public class HomeController : Controller
    {
        private TravelMakerEntities db = new TravelMakerEntities();
        //Data Source=res10;Initial Catalog=TravelMaker;Persist Security Info=True;User ID=travelmaker;Password=Aa2022032208;Application Name=EntityFramework
        //發佈用
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            if (Session[userDictionary.TM_LOGIN_USER] == null)
                return RedirectToAction("Login");
            return View();
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
                if(activeToken == p.email_ID.ToString())
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