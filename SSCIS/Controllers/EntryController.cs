using SSCIS.Class;
using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSCIS.Controllers
{
    /// <summary>
    /// Entry point controller for logging users in application
    /// </summary>
    public class EntryController : Controller
    {
        private SSCISEntities db = new SSCISEntities();
        private SSCISSessionManager sessionManager = new SSCISSessionManager();

        /// <summary>
        /// Default entry point
        /// </summary>
        /// <returns>Sets sesison with login is correct and redirects to Home/Index</returns>
        //[HttpPost]
        //public ActionResult Index()
        //{
        //    string login = Request.Headers["login"];
        //    if (login != null)
        //    {
        //        bool userExistInDatabase = db.SSCISUser.Where(o => o.IsActive.HasValue && o.IsActive.Value).Count() > 0;
        //        if (!userExistInDatabase)
        //        {
        //            SSCISUser user = new SSCISUser();
        //            user.Login = login;
        //            user.Role = db.Role.Where(r => r.RoleCode.Equals("USER")).Single();
        //            user.IsActive = true;
        //            user.Created = DateTime.Now;
        //            user.Activated = DateTime.Now;
        //            db.SSCISUser.Add(user);
        //            db.SaveChanges();
        //            sessionManager.SessionStart(user, Session);
        //        }
        //        else
        //        {
        //            SSCISUser user = db.SSCISUser.Where(u => u.Login.Equals(login)).Single();
        //            sessionManager.SessionStart(user, Session);
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        /// <summary>
        /// Default entry point
        /// TEST ONLY
        /// </summary>
        /// <returns>Sets sesison with login is correct and redirects to Home/Index</returns>
        [HttpGet]
        public ActionResult Index(string login)
        {
            if (login != null)
            {
                bool userExistInDatabase = db.SSCISUser.Where(o => o.IsActive.HasValue && o.IsActive.Value && o.Login.Equals(login)).Count() > 0;
                if (!userExistInDatabase)
                {
                    SSCISUser user = new SSCISUser();
                    user.Login = login;
                    user.Role = db.Role.Where(r => r.RoleCode.Equals("USER")).Single();
                    user.IsActive = true;
                    user.Created = DateTime.Now;
                    user.Activated = DateTime.Now;
                    db.SSCISUser.Add(user);
                    db.SaveChanges();
                    sessionManager.SessionStart(user.Login, Session);
                }
                else
                {
                    SSCISUser user = db.SSCISUser.Where(u => u.Login.Equals(login)).Single();
                    sessionManager.SessionStart(user.Login, Session);
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
