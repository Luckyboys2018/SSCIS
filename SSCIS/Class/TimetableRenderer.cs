using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SSCIS.Class
{
    /// <summary>
    /// Class for rendering timetable component
    /// </summary>
    public class TimetableRenderer
    {
        /// <summary>
        /// Renders timetable component
        /// </summary>
        /// <param name="events">list of events to display</param>
        /// <returns>rendered component</returns>
        public MvcHtmlString Render(List<Event> events)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table class=\"table\">");
            builder.Append("<tr>");
            builder.Append("<th>Čas od</th>");
            builder.Append("<th>Čas do</th>");
            builder.Append("<th>Poznámka ke zrušení</th>");
            builder.Append("<th>Předmět</th>");
            builder.Append("<th>Tutor</th>");
            builder.Append("</tr>");

            foreach (var item in events)
            {
                builder.Append(item.IsCancelled != null && item.IsCancelled.Value ? "<tr class=\"canceled-evnt\">" : "<tr>");
                builder.Append("<td>");
                builder.Append(item.TimeFrom.ToString("d") + " " + item.TimeFrom.ToString("t"));
                builder.Append("</td>");
                builder.Append("<td>");
                builder.Append(item.TimeTo.ToString("d") + " " + item.TimeTo.ToString("t"));
                builder.Append("</td>");
                builder.Append("<td>");
                builder.Append(item.CancellationComment);
                builder.Append("</td>");
                builder.Append("<td>");
                builder.Append(item.Subject.Code);
                builder.Append("</td>");
                builder.Append("<td>");
                builder.Append(item.Tutor.Login);
                builder.Append("</td>");
                builder.Append("</tr>");
            }

            builder.Append("</table>");
            return new MvcHtmlString(builder.ToString());
        }

        /// <summary>
        /// Renders public event timetable component
        /// </summary>
        /// <param name="db">Databes context</param>
        /// <returns>Html component</returns>
        public MvcHtmlString RenderPublic(SSCISEntities db, int weeks = 0)
        {
            DateTime now = DateTime.Now;
            now.AddDays(7 * weeks);
            DateTime start = _startOfWeek(now, DayOfWeek.Monday);
            DateTime end = start.AddDays(7);

            List<Event> events = db.Event.Where(e => e.TimeFrom >= start && e.TimeTo <= end && e.IsAccepted != null && e.IsAccepted.Value).OrderBy(e => e.TimeFrom).ToList();
            return Render(events);
        }

        /// <summary>
        /// Finds start day of week
        /// </summary>
        /// <param name="dt">date</param>
        /// <param name="startOfWeek">start day</param>
        /// <returns>start date</returns>
        private DateTime _startOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

    }
}