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

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Session["Role"] = "ADMIN";
            Session["userId"] = 1;
            return View();
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
