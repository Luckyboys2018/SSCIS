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

        /// <summary>
        /// Session manager
        /// </summary>
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
                    filterContext.Result = new RedirectResult(string.Format("{0}Home/Login", _addSlash(_getBaseUrl())));
                }
                var role = filterContext.HttpContext.Session["role"];
                if (role == null || !role.Equals(AccessLevel) && !role.Equals(AuthorizationRoles.Administrator))
                    filterContext.Result = new RedirectResult(string.Format("{0}Home/Unauthorized", _addSlash(_getBaseUrl())));
            }
        }

        /// <summary>
        /// Gets apps root url
        /// </summary>
        /// <returns>Root url</returns>
        private string _getBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }

        /// <summary>
        /// Adds slash at the end of URL if needed
        /// </summary>
        /// <param name="url">URL string</param>
        /// <returns>Url with slash at the end</returns>
        private string _addSlash(string url)
        {
            return url.EndsWith("/") ? url : url + "/";
        }
    }
}