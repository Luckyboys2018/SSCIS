using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SSCIS.Class
{
    /// <summary>
    /// Class with helper methods for view rendering
    /// </summary>
    public static class SSCISHtml
    {
        public const int HOURS = 24;
        public const int MINUTES = 60;

        /// <summary>
        /// Converts bool value to czech string
        /// </summary>
        /// <param name="value">Bool value</param>
        /// <returns>String representation</returns>
        public static string DisplayForBool(bool? value)
        {
            if (value == null) return "";
            return value.Value ? "Ano" : "Ne" ;
        }

        /// <summary>
        /// Creates timepicker component
        /// </summary>
        /// <param name="name">Name of component in form</param>
        /// <param name="minutesStep">Minutes step</param>
        /// <param name="htmlclass">Html class of component</param>
        /// <returns>Component in MvcHtmlString</returns>
        public static MvcHtmlString TimePicker(string name, int minutesStep, string htmlclass)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<select name=\"{0}\" class=\"{1}\">\n", name, htmlclass));
            for (int h = 0; h < HOURS; h++)
            {
                for (int m = 0; m < MINUTES; m += minutesStep)
                {
                    builder.Append(string.Format("\t<option value=\"{0}:{1}\">{0}:{1}</option>\n", h.ToString("00"), m.ToString("00")));
                }
            }
            builder.Append("</select>\n");
            return new MvcHtmlString(builder.ToString());
        }

        /// <summary>
        /// Parses timepicker value stored in request
        /// </summary>
        /// <param name="name">Name of timepicker</param>
        /// <param name="request">Http post request</param>
        /// <returns>Time</returns>
        public static DateTime TimePickerResult(string name, HttpRequestBase request)
        {
            string svalue = request.Form[name];
            int hours = int.Parse(svalue.Split(':')[0]);
            int minutes = int.Parse(svalue.Split(':')[1]);
            DateTime time = new DateTime(9999, 1, 1, hours, minutes, 0);
            return time;
        }

    }
}