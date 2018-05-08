using SSCIS.Class;
using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SSCIS.Controllers
{
    /// <summary>
    /// Entry point controller for logging users in application
    /// </summary>
    public class EntryController : Controller
    {
        /// <summary>
        /// Database context
        /// </summary>
        private SSCISEntities db = new SSCISEntities();

        /// <summary>
        /// Session manager
        /// </summary>
        private SSCISSessionManager sessionManager = new SSCISSessionManager();

        /// <summary>
        /// Username request key
        /// </summary>
        private const string USERNAME_KEY = "SHIB_REMOTEUSER";

        /// <summary>
        /// Email request key
        /// </summary>
        private const string EMAIL_KEY = "SHIB_EMAIL";

        /// <summary>
        /// WebAuth user verification key
        /// </summary>
        private const string WEB_AUTH_USER = "X-webauth_proxy_user";


        /// <summary>
        /// SSO Authentification
        /// </summary>
        /// <returns>HomePage</returns>
        public ActionResult Index()
        {
            if (Request.Headers[WEB_AUTH_USER] == null) return RedirectToAction("Index", "Home");
            string username = Request.Headers[USERNAME_KEY];
            var count = db.SSCISUser.Count(usr => usr.Login.Equals(username));
            if (count < 1)
            {
                //string email = Request.Headers[EMAIL_KEY];
                SSCISUser user = new SSCISUser();
                user.Created = DateTime.Now;
                user.Activated = DateTime.Now;
                user.Login = username;
                user.IsActive = true;
                //user.Email = email; //TODO dodat do db
                user.Role = db.Role.Where(r => r.RoleCode.Equals(AuthorizationRoles.User)).Single();
                db.SSCISUser.Add(user);
                db.SaveChanges();
            }

            int sessionId = new SSCISSessionManager().SessionStart(username, Session);

            ViewBag.SessionId = sessionId;
            SSCISSession session = db.SSCISSession.Find(sessionId);

            ViewBag.UserId = session.UserID;
            ViewBag.Hash = session.Hash;
            ViewBag.Role = session.User.Role.RoleCode;
            ViewBag.Login = session.User.Login;

            return View("Logged");

            //StringBuilder sb = new StringBuilder();
            //foreach (var key in Request.Headers.AllKeys)
            //{
            //    var val = Request.Headers[key];
            //    sb.Append(key);
            //    sb.Append(" = ");
            //    sb.Append(val);
            //    sb.Append("\n");
            //}
            //return Content(sb.ToString());
        }

        /// <summary>
        /// Info for testing purposes SSO authentification
        /// </summary>
        /// <returns>HomePage</returns>
        public ActionResult Info()
        {
            if (Request.Headers[WEB_AUTH_USER] == null) return RedirectToAction("Index", "Home");
            string username = Request.Headers[USERNAME_KEY];
            var count = db.SSCISUser.Count(usr => usr.Login.Equals(username));
            if (count < 1)
            {
                //string email = Request.Headers[EMAIL_KEY];
                SSCISUser user = new SSCISUser();
                user.Created = DateTime.Now;
                user.Activated = DateTime.Now;
                user.Login = username;
                user.IsActive = true;
                //user.Email = email; //TODO dodat do db
                user.Role = db.Role.Where(r => r.RoleCode.Equals(AuthorizationRoles.User)).Single();
                db.SSCISUser.Add(user);
                db.SaveChanges();
            }

            int sessionId = new SSCISSessionManager().SessionStart(username, Session);

            ViewBag.SessionId = sessionId;
            SSCISSession session = db.SSCISSession.Find(sessionId);

            ViewBag.UserId = session.UserID;
            ViewBag.Hash = session.Hash;
            ViewBag.Role = session.User.Role.RoleCode;
            ViewBag.Login = session.User.Login;

            return View("Logged");
            
            //return RedirectToAction("Index", "Home");

            //StringBuilder sb = new StringBuilder();
            //foreach (var key in Request.Headers.AllKeys)
            //{
            //    var val = Request.Headers[key];
            //    sb.Append(key);
            //    sb.Append(" = ");
            //    sb.Append(val);
            //    sb.Append("\n");
            //}
            //return Content(sb.ToString());
        }

        /// <summary>
        /// Stores parameters into users local session
        /// </summary>
        /// <param name="sessionId">Session ID</param>
        /// <param name="userId">User ID</param>
        /// <param name="hash">Hash</param>
        /// <param name="role">User role code</param>
        /// <param name="login">User login</param>
        /// <returns>Redirection to Homepage</returns>
        public ActionResult StoreData(int sessionId, int userId, string hash, string role, string login)
        {
            Session["sessionId"] = sessionId;
            Session["role"] = role;
            Session["hash"] = hash;
            Session["login"] = login;
            Session["userId"] = userId;
            return RedirectToAction("Index", "Home");
        }

    }
}
