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
    public class HomeController : Controller
    {

        private SSCISEntities db = new SSCISEntities();

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            SSCISContent model = db.SSCISContent.OrderByDescending(c => c.Created).First();
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.MapToken = db.SSCISParam.Where(p => p.ParamKey.Equals("MAP_TOKEN")).Single().ParamValue;
            ViewBag.Title = "Contact";
            return View();
        }

        public ActionResult HelpMe()
        {
            ViewBag.Title = "Potřebuji pomoc";
            return View();
        }

        public ActionResult News()
        {
            ViewBag.Title = "Novniky";
            MetaNews model = new MetaNews();
            model.Contents = db.SSCISContent.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Login(string validationMessage = null)
        {
            ViewBag.Title = "Login";
            MetaLogin model = new MetaLogin();
            model.ValidationMessage = validationMessage;
            return View(model);
        }

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

        [HttpGet]
        public ActionResult Logout()
        {
            new SSCISSessionManager().SessionDestroy((int)Session["sessionId"], Session);
            return RedirectToAction("Index");
        }

        //public async Task<ActionResult> Login()
        //{
        //    service = new WebAuthService.IMPL.WebAuthService();
        //    WebAuthParam model = await service.GenerateModel();
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(WebAuthParam webAuthParams)
        //{
        //    service = new WebAuthService.IMPL.WebAuthService();
        //    bool logged = await service.Authentificate(webAuthParams);
        //    ViewData["logged"] = logged;
        //    return View("ProcessLogin");
        //}
    }
}
