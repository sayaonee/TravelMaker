using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TravelMaker.Models;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;

namespace TravelMaker.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private TravelMakerEntities db = new TravelMakerEntities();
        // GET api/Account/GetRegister
        [HttpGet]
        public IQueryable GetRegister()
        {
            var accounts = db.Account.Select(p => new
            {
                p.userAccount,
                p.roleAdmin,
                p.userEmail,
                p.email_Approved
            });

            return accounts;
        }
        // GET api/Account/GetAccountDetail/1
        [HttpGet]
        public IHttpActionResult GetAccountDetail(int id)
        {
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return NotFound();
            }
            account.userPassword = null;
            return Ok(account);
        }
        [HttpPost]
        // POST: api/Account/PostRegister
        [ResponseType(typeof(Account))]
        public IHttpActionResult PostRegister(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool accountsChecked = true;
            var accountsCheck = db.Account.Select(p => new
            {
                p.userAccount,
                p.userEmail,
            });
            foreach(var p in accountsCheck)
            {
                if (p.userAccount == account.userAccount)
                {
                    accountsChecked = false;
                }
                if (p.userEmail == account.userEmail)
                {
                    accountsChecked = false;
                }
            }
            if (account.userAccount == null)
            {
                accountsChecked = false;
            }
            if (account.userPassword == null)
            {
                accountsChecked = false;
            }
            if (account.userEmail == null)
            {
                accountsChecked = false;
            }
            if (account.userPhone == null)
            {
                accountsChecked = false;
            }
            if (accountsChecked)
            {
                account.roleAdmin = 1;
                account.email_Approved = "N";
                account.userFavorite = "[]";
                account.registerDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                account.email_ID = Guid.NewGuid();
                db.Account.Add(account);
                db.SaveChanges();
                //信箱驗證系統
                string activeAddress = $"http://20.210.96.122/home/active?activeToken={account.email_ID}";
                string mailContent = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Active.html"));
                mailContent = mailContent.Replace("myActiveAddress", activeAddress);
                SendMail(account, mailContent);
                return CreatedAtRoute("ActionApi", new { id = account.user_Id }, account);

            }
            else
            {
                return Ok("帳號或信箱已註冊");
            }
        }
        // GET api/Account/RetryActiveEmail?email=
        [HttpGet]
        public IHttpActionResult RetryActiveEmail(string email)
        {
            bool emailChecked = false;
            int targetUserId = 0;
            var accounts = db.Account;
            foreach (var p in accounts)
            {
                if (p.userEmail == email)
                {
                    emailChecked = true;
                    targetUserId = p.user_Id;
                }
            }
            if (emailChecked == false)
            {
                return NotFound();
            }
            Account account = db.Account.Find(targetUserId);
            if (account.email_Approved == "Y")
            {
                return Ok("帳號已經驗證過");
            }
            string activeAddress = $"http://20.210.96.122/home/active?activeToken={account.email_ID}";
            string mailContent = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Active.html"));
            mailContent = mailContent.Replace("myActiveAddress", activeAddress);
            SendMail(account, mailContent);
            return CreatedAtRoute("ActionApi", account.user_Id, "已重新發送驗證郵件");
        }
        // GET api/Account/RetryActiveEmail/1
        [HttpGet]
        public IHttpActionResult ReplyPassword(string email)
        {
            bool emailChecked = false;
            int targetUserId = 0;
            var accounts = db.Account;
            foreach (var p in accounts)
            {
                if (p.userEmail == email)
                {
                    emailChecked = true;
                    targetUserId = p.user_Id;
                }
            }
            if (emailChecked == false)
            {
                return NotFound();
            }
            Account account = db.Account.Find(targetUserId);
            if (account == null)
            {
                return NotFound();
            }
            if (account.email_Approved == "N")
            {
                return Ok("帳號還未驗證過");
            }
            var accountById = db.Account.FirstOrDefault(p => p.user_Id == account.user_Id);
            Random rnd = new Random();
            int newPassword = rnd.Next(10000, 99999);
            string mailContent = $"您的新密碼為:{newPassword}<br>請盡速進入帳號頁面更新密碼!";
            accountById.userPassword = newPassword.ToString();
            db.SaveChanges();
            SendMail(account, mailContent);
            return CreatedAtRoute("ActionApi", account.user_Id , "已發送新密碼");
        }
        private string SendMail(Account account, string mailContent)
        {
            // Google 發信帳號密碼
            string GoogleMailUserID = ConfigurationManager.AppSettings["GoogleMailUserID"];
            string GoogleMailUserPwd = ConfigurationManager.AppSettings["GoogleMailUserPwd"];
            string mailTile = $"來自TravelMaker的{account.userAccount}使用者驗證信";
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";  //設定Server
            client.Port = 587;  //設定Port
            client.Credentials = new NetworkCredential(GoogleMailUserID, GoogleMailUserPwd);  //設定寄件人的帳號密碼
            client.EnableSsl = true;  //是否啟用SSL驗證
            //傳入寄件人與收信人的MailAddress物件
            MailMessage mail = new MailMessage(GoogleMailUserID, account.userEmail);
            //發信人，這裡一定要使用MailAddress物件，且發信人地址會和client.Credentials設定的帳號相同
            mail.From = new MailAddress(GoogleMailUserID, "TravelMaker網站管理方(請勿回覆)");
            //設定收件人，可以為字串
            mail.To.Add(account.userEmail);
            //設定標題
            mail.Subject = mailTile;
            //標題編碼
            mail.SubjectEncoding = Encoding.UTF8;
            //是否使用html當作信件內容主體
            mail.IsBodyHtml = true;
            //信件內容 
            mail.Body = mailContent;
            //內容編碼
            mail.BodyEncoding = Encoding.UTF8;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                mail.Dispose();
                client.Dispose();
            }
            return "OK";
        }
        [HttpPost]
        // POST api/Account/PostAccountDetail
        [ResponseType(typeof(Account))]
        public IHttpActionResult PostAccountDetail(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accountById = db.Account.FirstOrDefault(p => p.user_Id == account.user_Id);
            if (account.userAccount != null)
            {
                accountById.userAccount = account.userAccount;
            }
            if (account.userPassword != null)
            {
                accountById.userPassword = account.userPassword;
            }
            if (account.userPhone != null)
            {
                accountById.userPhone = account.userPhone;
            }
            if (account.userEmail != null)
            {
                accountById.userEmail = account.userEmail;
            }
            if (account.userPhoto != null)
            {
                accountById.userPhoto = account.userPhoto;
            }
            if (account.userFavorite != null)
            {
                accountById.userFavorite = account.userFavorite;
            }
            db.SaveChanges();
            return CreatedAtRoute("ActionApi", new { id = account.user_Id }, account);
        }
        [HttpPost]
        // POST: api/Account/Login
        [ResponseType(typeof(Account))]
        public IHttpActionResult Login(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool userCheck = false;
            string targetAccount = null;
            var accounts = db.Account;
            foreach (var p in accounts)
            {
                if (account.userAccount == p.userAccount && account.userPassword == p.userPassword)
                {
                    userCheck = true;
                    targetAccount = account.userAccount;
                }
            }
            if (userCheck)
            {
                var accountSimple = db.Account.Select(p => new
                {
                    p.user_Id,
                    p.userAccount,
                    p.roleAdmin,
                    p.userEmail,
                    p.email_Approved,
                    p.userPhoto,
                    p.userFavorite,
                    p.travelOwner
                });
                var accountResult = accountSimple.FirstOrDefault(p => p.userAccount == targetAccount);
                return CreatedAtRoute("ActionApi", accountResult.user_Id , accountResult);
            }
            else
            {
                return NotFound();
            }
        }
        //暫時不用
        // DELETE: api/Shippers/5
        //[ResponseType(typeof(Account))]
        //public IHttpActionResult DeleteAccount(int id)
        //{
        //    Account account = db.Account.Find(id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Account.Remove(account);
        //    db.SaveChanges();

        //    return Ok(account);
        //}
    }
}