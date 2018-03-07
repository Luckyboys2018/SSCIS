using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSCIS.Attributes
{
    public class SSCISAuthorize : ActionFilterAttribute
    {
        public string AccessLevel { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AccessLevel != null)
            {
                var role = filterContext.HttpContext.Session["Role"];
                if (role == null || !role.Equals(AccessLevel))
                    filterContext.Result = new RedirectResult(string.Format("/Home/Login", filterContext.HttpContext.Request.Url.AbsolutePath));
            }
        }
    }
}