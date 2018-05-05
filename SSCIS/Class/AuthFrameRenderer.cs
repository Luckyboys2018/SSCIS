using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSCIS.Class
{
    /// <summary>
    /// Class for rendering authentification frame
    /// </summary>
    public static class AuthFrameRenderer
    {
        /// <summary>
        /// Parameter key
        /// </summary>
        private const string URL_KEY = "AUTH_FRAME_URL";

        /// <summary>
        /// Renders frame with authentification form
        /// </summary>
        /// <param name="db">Database context</param>
        /// <returns>Rendered frame component</returns>
        public static MvcHtmlString Render(SSCISEntities db)
        {
            SSCISParam param = db.SSCISParam.Where(p => p.ParamKey.Equals(URL_KEY)).Single();
            return new MvcHtmlString("<iframe id=\"auth-frame\" src=\""+ param.ParamValue + "\"></iframe>");
        }
    }
}