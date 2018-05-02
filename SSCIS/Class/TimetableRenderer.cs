using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SSCIS.Class
{
    /// <summary>
    /// Class for rendering timetable component
    /// </summary>
    public class TimetableRenderer
    {
        private static string[] TIMES = new string[] {
            "8:00","8:15","8:30","8:45",
            "9:00","9:15","9:30","9:45",
            "10:00","10:15","10:30","10:45",
            "11:00","11:15","11:30","11:45",
            "12:00","12:15","12:30","12:45",
            "13:00","13:15","13:30","13:45",
            "14:00","14:15","14:30","14:45",
            "15:00","15:15","15:30","15:45",
            "16:00","16:15","16:30","16:45",
            "17:00","17:15","17:30","17:45",
            "18:00"
        };

        /// <summary>
        /// Renders timetable component
        /// </summary>
        /// <param name="events">list of events to display</param>
        /// <returns>rendered component</returns>
        public string Render(List<Event> events)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (Event e in events)
            {
                if (!dates.Contains(e.TimeFrom.Date))
                {
                    dates.Add(e.TimeFrom.Date);
                }
            }


            StringBuilder builder = new StringBuilder();
            builder.Append("<table>\n");
            builder.Append("<tr>\n");
            builder.Append("<td>Datum</td>\n");
            foreach (string time in TIMES)
            {
                builder.Append("<td>" + time + "</td>");
            }
            builder.Append("</tr>\n");
            builder.Append("</table>\n");

            //TODO

            return builder.ToString();
        }

    }
}