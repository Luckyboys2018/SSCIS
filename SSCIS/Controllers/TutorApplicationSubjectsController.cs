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
    public class TutorApplicationSubjectsController : Controller
    {
        private SSCISEntities db = new SSCISEntities();

        // GET: TutorApplicationSubjects
        public ActionResult Index()
        {
            var tutorApplicationSubject = db.TutorApplicationSubject.Include(t => t.Subject).Include(t => t.Application);
            return View(tutorApplicationSubject.ToList());
        }

        // GET: TutorApplicationSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorApplicationSubject tutorApplicationSubject = db.TutorApplicationSubject.Find(id);
            if (tutorApplicationSubject == null)
            {
                return HttpNotFound();
            }
            return View(tutorApplicationSubject);
        }

        // GET: TutorApplicationSubjects/Create
        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.Subject, "ID", "Code");
            ViewBag.ApplicationID = new SelectList(db.TutorApplication, "ID", "ID");
            return View();
        }

        // POST: TutorApplicationSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SubjectID,ApplicationID,Degree")] TutorApplicationSubject tutorApplicationSubject)
        {
            if (ModelState.IsValid)
            {
                db.TutorApplicationSubject.Add(tutorApplicationSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.Subject, "ID", "Code", tutorApplicationSubject.SubjectID);
            ViewBag.ApplicationID = new SelectList(db.TutorApplication, "ID", "ID", tutorApplicationSubject.ApplicationID);
            return View(tutorApplicationSubject);
        }

        // GET: TutorApplicationSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorApplicationSubject tutorApplicationSubject = db.TutorApplicationSubject.Find(id);
            if (tutorApplicationSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.Subject, "ID", "Code", tutorApplicationSubject.SubjectID);
            ViewBag.ApplicationID = new SelectList(db.TutorApplication, "ID", "ID", tutorApplicationSubject.ApplicationID);
            return View(tutorApplicationSubject);
        }

        // POST: TutorApplicationSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SubjectID,ApplicationID,Degree")] TutorApplicationSubject tutorApplicationSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorApplicationSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.Subject, "ID", "Code", tutorApplicationSubject.SubjectID);
            ViewBag.ApplicationID = new SelectList(db.TutorApplication, "ID", "ID", tutorApplicationSubject.ApplicationID);
            return View(tutorApplicationSubject);
        }

        // GET: TutorApplicationSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorApplicationSubject tutorApplicationSubject = db.TutorApplicationSubject.Find(id);
            if (tutorApplicationSubject == null)
            {
                return HttpNotFound();
            }
            return View(tutorApplicationSubject);
        }

        // POST: TutorApplicationSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TutorApplicationSubject tutorApplicationSubject = db.TutorApplicationSubject.Find(id);
            db.TutorApplicationSubject.Remove(tutorApplicationSubject);
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
