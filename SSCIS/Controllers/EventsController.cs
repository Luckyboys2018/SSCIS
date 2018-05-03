using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSCIS.Models;
using SSCIS.Class;
using SSCIS.Models.Meta;
using SSCIS.Attributes;

namespace SSCIS.Controllers
{
    /// <summary>
    /// Events controller
    /// </summary>
    public class EventsController : Controller
    {
        /// <summary>
        /// Database context
        /// </summary>
        private SSCISEntities db = new SSCISEntities();

        /// <summary>
        /// Timetable component renderer
        /// </summary>
        private TimetableRenderer timetableRenderer = new TimetableRenderer();

        /// <summary>
        /// List of created events
        /// </summary>
        /// <returns>View with list of events</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Tutor)]
        public ActionResult Index()
        {
            DateTime now = DateTime.Now;
            var eventModel = db.Event.Where(e => e.TimeFrom > now).Include(@e => @e.Subject).Include(@e => @e.Tutor);
            return View(eventModel.ToList());
        }

        #region Unused
        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }
        #endregion

        /// <summary>
        /// Creates new event
        /// </summary>
        /// <returns>Form view</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Tutor)]
        public ActionResult Create()
        {
            int userId = (int)Session["userID"];
            List<Approval> approvals = db.Approval.Where(a => a.TutorID == userId).ToList();
            List<int> subjectsIds = new List<int>();
            foreach (Approval app in approvals)
            {
                subjectsIds.Add(app.SubjectID);
            }
            ViewBag.SubjectID = new SelectList(db.Subject.Where(s => subjectsIds.Contains(s.ID)), "ID", "Code");
            ViewBag.TutorID = new SelectList(db.SSCISUser.Where(t => t.ID == userId), "ID", "Login");
            return View();
        }

        /// <summary>
        /// Cretes new event
        /// </summary>
        /// <param name="@event">Event model</param>
        /// <returns>Creation result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Tutor)]
        public ActionResult Create(MetaEvent model)
        {
            model.Event = new Event();
            if (ModelState.IsValid)
            {
                int userId = (int)Session["userID"];
                model.Event.Tutor = db.SSCISUser.Find(userId);
                model.Event.IsCancelled = false;
                model.Event.IsAccepted = false;
                model.Event.TimeFrom = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, model.TimeFrom.Hour, model.TimeFrom.Minute, 0);
                model.Event.TimeFrom = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, model.TimeTo.Hour, model.TimeTo.Minute, 0);
                model.Event.Subject = db.Subject.Find(model.SubjectID);
                db.Event.Add(model.Event);
                db.SaveChanges();
                return RedirectToAction("TutorEvents");
            }
            return RedirectToAction("Create");
        }

        /// <summary>
        /// Acceptation of created event
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns>Redirection to list of events</returns>
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        [HttpGet]
        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            @event.IsAccepted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Cancellation of created event form
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns>View with form</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        /// <summary>
        /// Cancellation of created event
        /// </summary>
        /// <param name="model">Event model</param>
        /// <returns>Redirection to list</returns>
        [HttpPost]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Cancel(Event model)
        {
            Event @event = db.Event.Find(model.ID);
            if (@event == null)
            {
                return HttpNotFound();
            }
            @event.IsCancelled = true;
            @event.CancellationComment = model.CancellationComment;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Shows tutors events
        /// </summary>
        /// <returns>Tutors events view</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Tutor)]
        public ActionResult TutorEvents()
        {
            int userId = (int)Session["userID"];
            ViewBag.TutorEventsTable = timetableRenderer.RenderTutor(db, userId);
            return View();
        }

        #region Unused
        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.Subject, "ID", "Code", @event.SubjectID);
            ViewBag.TutorID = new SelectList(db.SSCISUser, "ID", "Login", @event.TutorID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TimeFrom,TimeTo,SubjectID,TutorID,IsAccepted,IsCancelled,CancellationComment")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.Subject, "ID", "Code", @event.SubjectID);
            ViewBag.TutorID = new SelectList(db.SSCISUser, "ID", "Login", @event.TutorID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Event.Find(id);
            db.Event.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        /// <summary>
        /// Disposed controller
        /// </summary>
        /// <param name="disposing">dispose db context</param>
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
