using SSCIS.Attributes;
using SSCIS.Class;
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
        /// <summary>
        /// Database context
        /// </summary>
        private SSCISEntities db = new SSCISEntities();

        /// <summary>
        /// Time table components renderer
        /// </summary>
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
        [HttpGet]
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
                model.Created = DateTime.Now;
                model.TextContent = "Žádná aktualita nebyla nalezena";
            }
            ViewBag.PublicTimeTable = timeTableRenderer.RenderPublic(db);
            return View(model);
        }

        /// <summary>
        /// Contact
        /// </summary>
        /// <returns>Contact view</returns>
        [HttpGet]
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
        [HttpGet]
        public ActionResult HelpMe()
        {
            ViewBag.Title = "Potřebuji pomoc";
            ViewBag.PublicTimeTable = timeTableRenderer.RenderPublic(db);
            return View();
        }

        /// <summary>
        /// News
        /// </summary>
        /// <returns>View with news</returns>
        [HttpGet]
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
            bool webauth = BoolParser.Parse(db.SSCISParam.Where(p => p.ParamKey.Equals(SSCISParameters.WEB_AUTH_ON)).Single().ParamValue);
            if (webauth)
            {
                return Redirect(db.SSCISParam.Where(p => p.ParamKey.Equals(SSCISParameters.WEB_AUTH_URL)).Single().ParamValue);
            }
            ViewBag.Title = "Login";
            MetaLogin model = new MetaLogin();
            model.ValidationMessage = validationMessage;
            return View(model);
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
        [HttpGet]
        public ActionResult Unauthorized()
        {
            return View();
        }

        /// <summary>
        /// Signpost for statistics
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Statistics()
        {
            return View();
        }

        /// <summary>
        /// Checks version of application
        /// </summary>
        /// <returns>Version of application</returns>
        [HttpGet]
        public ActionResult Version()
        {
            string version = db.SSCISParam.Where(p => p.ParamKey.Equals(SSCISParameters.VERSION)).Single().ParamValue;
            return Content(version);
        }

    }
}
