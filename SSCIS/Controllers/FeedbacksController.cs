using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSCIS.Models;
using System.IO;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using SSCIS.Class;
using SSCIS.Attributes;
using SSCIS.Models.Meta;

namespace SSCIS.Controllers
{
    /// <summary>
    /// Feedback controller
    /// </summary>
    public class FeedbacksController : Controller
    {
        /// <summary>
        /// Database context
        /// </summary>
        private SSCISEntities db = new SSCISEntities();

        /// <summary>
        /// Feedback QR code URL generator
        /// </summary>
        private FeedbackUrlGenerator urlGenerator = new FeedbackUrlGenerator();

        /// <summary>
        /// Feedbacks to CSV converte
        /// </summary>
        private FeedbacksCSVConverter csvConverter = new FeedbacksCSVConverter();

        /// <summary>
        /// Adding new feedback
        /// </summary>
        /// <param name="code">Code from url</param>
        /// <returns>Feedback form view</returns>
        [HttpGet]
        public ActionResult Index(string code)
        {
            int? eventId = urlGenerator.ResolveEventID(code);
            if (eventId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback model = new Feedback() { ID = eventId.Value }; //sending eventID in feedbackID for POST
            return View(model);
        }

        /// <summary>
        /// Saves feedback
        /// </summary>
        /// <param name="model">Feedback model</param>
        /// <returns>Redirection</returns>
        [HttpPost]
        public ActionResult Index(Feedback model)
        {
            int eventID = model.ID;
            Event evnt = db.Event.Find(eventID);
            Feedback feedback = new Feedback() { Text = model.Text };
            Participation part = new Participation() { Event = evnt };

            db.Participation.Add(part);
            db.SaveChanges();
            feedback.Participation = part;
            db.Feedback.Add(feedback);
            db.SaveChanges();

            return RedirectToAction("Sent");
        }

        /// <summary>
        /// Message after sent feedback
        /// </summary>
        /// <returns>View with message</returns>
        [HttpGet]
        public ActionResult Sent()
        {
            return View();
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        /// <summary>
        /// Generates QR code of URL for adding feedback to event
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns>View with QR code of URL</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Tutor)]
        public ActionResult EventQR(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                string url = urlGenerator.GenerateURL(id.Value, db);
                ViewBag.FeedbackURL = url;
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }

        /// <summary>
        /// Creates view with form with filter for generating csv file with feedback
        /// </summary>
        /// <returns>View with form</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Generate()
        {
            return View();
        }

        /// <summary>
        /// Return csv file with feedbacks
        /// </summary>
        /// <param name="model">Filter model</param>
        /// <returns>CSV file</returns>
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Generate")]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Generate(MetaInterval model)
        {
            List<Feedback> feedbacks = db.Feedback.Where(f => f.Participation.Event.TimeFrom >= model.From && f.Participation.Event.TimeTo <= model.To).ToList();
            string csv = csvConverter.Convert(feedbacks, db);
            string filename = string.Format("feedback.csv");
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", filename);
        }

        /// <summary>
        /// Shows list of feedbacks
        /// </summary>
        /// <param name="model">Interval model</param>
        /// <returns>View with list of feedback</returns>
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "List")]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult List(MetaInterval model)
        {
            List<Feedback> feedbacks = db.Feedback.Where(f => f.Participation.Event.TimeFrom >= model.From && f.Participation.Event.TimeTo <= model.To).ToList();
            return View(model);
        }

        /// <summary>
        /// Disposes cotroller
        /// </summary>
        /// <param name="disposing">Dispose db context</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
