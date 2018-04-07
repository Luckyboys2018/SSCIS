using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSCIS.Models;

namespace SSCIS.Controllers
{
    public class EventsController : Controller
    {
        private SSCISEntities db = new SSCISEntities();

        // GET: Events
        public ActionResult Index()
        {
            var eventModel = db.Event.Include(@e => @e.Subject).Include(@e => @e.Tutor);
            return View(eventModel.ToList());
        }

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

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.Subject, "ID", "Code");
            ViewBag.TutorID = new SelectList(db.SSCISUser, "ID", "Login");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TimeFrom,TimeTo,SubjectID,TutorID,IsAccepted,IsCancelled,CancellationComment")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.IsCancelled = false;
                @event.IsAccepted = false;
                db.Event.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.Subject, "ID", "Code", @event.SubjectID);
            ViewBag.TutorID = new SelectList(db.SSCISUser, "ID", "Login", @event.TutorID);
            return View(@event);
        }

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
