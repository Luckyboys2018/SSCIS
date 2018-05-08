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
        /// SSO Authentification
        /// </summary>
        /// <returns>HomePage</returns>
        public ActionResult Index()
        {
            string username = Request.Headers[USERNAME_KEY];
            var count = db.SSCISUser.Count(usr => usr.Login.Equals(username));
            if (count < 1)
            {
                string email = Request.Headers[EMAIL_KEY];
                SSCISUser user = new SSCISUser();
                user.Created = DateTime.Now;
                user.Activated = DateTime.Now;
                user.Login = username;
                //user.Email = email; //TODO dodat do db
                user.Role = db.Role.Where(r => r.RoleCode.Equals(AuthorizationRoles.User)).Single();
                db.SSCISUser.Add(user);
                db.SaveChanges();
            }
            new SSCISSessionManager().SessionStart(username, Session);
            return RedirectToAction("Index", "Home");

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
            string username = Request.Headers[USERNAME_KEY];
            var count = db.SSCISUser.Count(usr => usr.Login.Equals(username));
            if (count < 1)
            {
                string email = Request.Headers[EMAIL_KEY];
                SSCISUser user = new SSCISUser();
                user.Created = DateTime.Now;
                user.Login = username;
                //user.Email = email; //TODO dodat do db
                user.Role = db.Role.Where(r => r.RoleCode.Equals(AuthorizationRoles.User)).Single();
                db.SSCISUser.Add(user);
                db.SaveChanges();
            }
            new SSCISSessionManager().SessionStart(username, Session);
            return RedirectToAction("Index", "Home");

            //StringBuilder sb = new StringBuilder();
            //foreach (var key in Request.Headers.AllKeys)
            //{
            //    var val = Request.Headers[key];
            //    sb.Append(key);
            //    sb.Append(" = ");
            //    sb.Append(val);
            //    sb.Append("\n");
            //}
            //ViewData["infoContent"] = sb.ToString();
            //return View();
        }

    }
}
