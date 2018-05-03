using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SSCIS.Class
{
    /// <summary>
    /// Class for feedbacks to csv format conversion
    /// </summary>
    public class FeedbacksCSVConverter
    {

        /// <summary>
        /// CSV header
        /// </summary>
        private const string CSV_HEADER = "Datum|Od|Do|Předmět|Text";


        /// <summary>
        /// Converts list of feedbacks to CSV string
        /// </summary>
        /// <param name="feedbacks">List of feedbacks</param>
        /// <param name="db">Database context</param>
        /// <returns>String with CSV content</returns>
        public string Convert(List<Feedback> feedbacks, SSCISEntities db)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(CSV_HEADER);
            builder.Append("\n");
            foreach (Feedback feedback in feedbacks)
            {
                Participation participation = db.Participation.Find(feedback.ParticipationID);
                Event @event = db.Event.Find(participation.EventID);
                builder.Append(FeedbackToString(feedback, @event));
                builder.Append("\n");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Converts feedback to string CSV representation
        /// </summary>
        /// <param name="feedback">Feedback model</param>
        /// <param name="@event">Event model</param>
        /// <returns>String representation of feedback</returns>
        public string FeedbackToString(Feedback feedback, Event @event)
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}", @event.TimeFrom.Day + "." + @event.TimeFrom.Month + "." + @event.TimeFrom.Year, @event.TimeFrom.Hour.ToString("00") + ":" + @event.TimeFrom.Minute.ToString("00"), @event.TimeTo.Hour.ToString("00") + ":" + @event.TimeTo.Minute.ToString("00"), @event.Subject.Code, feedback.Text);
        }
    }
}