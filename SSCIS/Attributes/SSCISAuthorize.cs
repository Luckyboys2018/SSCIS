using SSCIS.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSCIS.Attributes
{
    /// <summary>
    /// Attribute for authorizing controllers actionResult methods
    /// </summary>
    public class SSCISAuthorize : ActionFilterAttribute
    {
        public string AccessLevel { get; set; }

        private SSCISSessionManager _sessionManager = new SSCISSessionManager();

        /// <summary>
        /// Contructor
        /// </summary>
        public SSCISAuthorize()
        {
            _sessionManager = new SSCISSessionManager();
        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="sessionManager">Session manager</param>
        public SSCISAuthorize(SSCISSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        /// <summary>
        /// Authorizes actionResultMethod
        /// </summary>
        /// <param name="filterContext">authoentification filter context</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AccessLevel != null)
            {
                if (!_sessionManager.VerifySession(filterContext.HttpContext.Session))
                {
                    filterContext.Result = new RedirectResult(string.Format("/Home/Login", filterContext.HttpContext.Request.Url.AbsolutePath));
                }
                var role = filterContext.HttpContext.Session["role"];
                if (role == null || !role.Equals(AccessLevel))
                    filterContext.Result = new RedirectResult(string.Format("/Home/Login", filterContext.HttpContext.Request.Url.AbsolutePath));
            }
        }
    }
}