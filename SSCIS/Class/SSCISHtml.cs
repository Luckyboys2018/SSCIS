using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SSCIS.Class
{
    /// <summary>
    /// Class with helper methods for view rendering
    /// </summary>
    public static class SSCISHtml
    {
        public static string DisplayForBool(bool? value)
        {
            if (value == null) return "";
            return value.Value ? "Ano" : "Ne" ;
        }
    }
}