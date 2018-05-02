﻿using SSCIS.Class;
using SSCIS.Models;
using SSCIS.Models.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SSCIS.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {
        //Database context
        private SSCISEntities db = new SSCISEntities();

        private TimetableRenderer timeTableRenderer = new TimetableRenderer();

        /// <summary>
        /// Inicialization of controller
        /// </summary>
        /// <param name="requestContext">Context of http request</param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        /// <summary>
        /// Home page
        /// </summary>
        /// <returns>Home page view</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            SSCISContent model = null;
            if (db.SSCISContent.Count() > 0)
            {
                model = db.SSCISContent.OrderByDescending(c => c.Created).First();
            }
            else
            {
                model = new SSCISContent();
                //model.Created = DateTime.Now;
                model.TextContent = "Žádná aktualita nebyla nalezena";
            }
            ViewBag.PublicTimeTable = timeTableRenderer.RenderPublic(db);
            return View(model);
        }

        /// <summary>
        /// Contact
        /// </summary>
        /// <returns>Contact view</returns>
        public ActionResult Contact()
        {
            ViewBag.MapToken = db.SSCISParam.Where(p => p.ParamKey.Equals("MAP_TOKEN")).Single().ParamValue;
            ViewBag.Title = "Contact";
            return View();
        }

        #region Unused
        public ActionResult Username()
        {
            ViewBag.Title = "Username - Profil";
            return View();
        }
        #endregion

        /// <summary>
        /// Help me view
        /// </summary>
        /// <returns>View</returns>
        public ActionResult HelpMe()
        {
            ViewBag.Title = "Potřebuji pomoc";
            return View();
        }

        /// <summary>
        /// News
        /// </summary>
        /// <returns>View with news</returns>
        public ActionResult News()
        {
            ViewBag.Title = "Novniky";
            MetaNews model = new MetaNews();
            model.Contents = db.SSCISContent.OrderByDescending(c => c.Created).ToList();
            return View(model);
        }

        /// <summary>
        /// Login UC
        /// </summary>
        /// <param name="validationMessage">validation message</param>
        /// <returns>View with login form</returns>
        [HttpGet]
        public ActionResult Login(string validationMessage = null)
        {
            ViewBag.Title = "Login";
            MetaLogin model = new MetaLogin();
            model.ValidationMessage = validationMessage;
            return View(model);
            //return Redirect("https://fkmagion.zcu.cz/testauth");
        }

        /// <summary>
        /// Login process
        /// </summary>
        /// <param name="model">Data from login view</param>
        /// <returns>Redirection</returns>
        [HttpPost]
        public ActionResult Login(MetaLogin model)
        {
            var count = db.SSCISUser.Count(usr => usr.Login.Equals(model.Login));
            if (count == 1)
            {
                new SSCISSessionManager().SessionStart(model.Login, Session);
                return RedirectToAction("Index");
            }
            return Login("Invalid login");
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns>Redirection</returns>
        [HttpGet]
        public ActionResult Logout()
        {
            new SSCISSessionManager().SessionDestroy((int)Session["sessionId"], Session);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Unauthorized access view
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Unauthorized()
        {
            return View();
        }

    }
}
