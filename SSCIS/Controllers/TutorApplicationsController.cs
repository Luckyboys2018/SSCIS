using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSCIS.Models;
using SSCIS.Models.Meta;

namespace SSCIS.Controllers
{
    public class TutorApplicationsController : Controller
    {
        private SSCISEntities db = new SSCISEntities();

        // GET: TutorApplications
        public ActionResult Index()
        {
            var tutorApplication = db.TutorApplication.Include(t => t.AcceptedBy).Include(t => t.Applicant).Where(t => t.IsAccepted == null);
            return View(tutorApplication.ToList());
        }

        // GET: TutorApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorApplication tutorApplication = db.TutorApplication.Find(id);
            if (tutorApplication == null)
            {
                return HttpNotFound();
            }
            MetaTutorApplication model = new MetaTutorApplication();
            model.Application = tutorApplication;
            List<TutorApplicationSubject> subjects = db.TutorApplicationSubject.Where(s => s.ApplicationID == tutorApplication.ID).ToList();
            model.ApplicationSubjects = subjects;
            return View(model);
        }

        // GET: TutorApplications/Create
        public ActionResult Create(int count = 1)
        {
            if (Session["role"] == null) return View("Create_public");
            MetaTutorApplication model = new MetaTutorApplication();
            for (int i = 0; i < count; i++)
            {
                model.ApplicationSubjects.Add(new TutorApplicationSubject());
            }
            int? userID = (int)Session["userID"];
            SSCISUser user = db.SSCISUser.Find(userID);
            model.Application.Applicant = user;
            ViewBag.SubjectID = new SelectList(db.Subject.Where(s => s.Lesson != null && !s.Lesson.Value).ToList(), "ID", "Code");
            ViewBag.AcceptedByID = new SelectList(db.SSCISUser, "ID", "Login");
            ViewBag.UserID = new SelectList(db.SSCISUser, "ID", "Login");
            int?[] degrees = new int?[] { null, 1, 2, 3, 4 };
            ViewBag.Degree = new SelectList(degrees);
            ViewBag.NextCount = count + 1;
            return View(model);
        }

        // POST: TutorApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "ID,UserID,ApplicationDate,IsAccepted,AcceptedDate,AcceptedByID")] TutorApplication tutorApplication*/ MetaTutorApplication model)
        {
            if (ModelState.IsValid)
            {
                model.Application.IsAccepted = false;
                model.Application.ApplicationDate = DateTime.Now;
                db.TutorApplication.Add(model.Application);
                db.SaveChanges();

                foreach (TutorApplicationSubject subject in model.ApplicationSubjects)
                {
                    subject.Application = model.Application;
                    db.TutorApplicationSubject.Add(subject);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.AcceptedByID = new SelectList(db.SSCISUser, "ID", "Login", model.Application.AcceptedByID);
            ViewBag.UserID = new SelectList(db.SSCISUser, "ID", "Login", model.Application.UserID);
            return View(model.Application);
        }

        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorApplication tutorApplication = db.TutorApplication.Find(id);
            if (tutorApplication == null)
            {
                return HttpNotFound();
            }
            tutorApplication.AcceptedDate = DateTime.Now;
            tutorApplication.IsAccepted = true;
            tutorApplication.AcceptedBy = db.SSCISUser.Find(Session["userId"]);
            db.SaveChanges();
            tutorApplication.Applicant.Role = db.Role.Where(r => "TUTOR".Equals(r.RoleCode)).Single();
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Decline(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorApplication tutorApplication = db.TutorApplication.Find(id);
            if (tutorApplication == null)
            {
                return HttpNotFound();
            }
            tutorApplication.AcceptedDate = DateTime.Now;
            tutorApplication.IsAccepted = false;
            tutorApplication.AcceptedBy = db.SSCISUser.Find(Session["userId"]);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: TutorApplications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorApplication tutorApplication = db.TutorApplication.Find(id);
            if (tutorApplication == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcceptedByID = new SelectList(db.SSCISUser, "ID", "Login", tutorApplication.AcceptedByID);
            ViewBag.UserID = new SelectList(db.SSCISUser, "ID", "Login", tutorApplication.UserID);
            return View(tutorApplication);
        }

        // POST: TutorApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,ApplicationDate,IsAccepted,AcceptedDate,AcceptedByID")] TutorApplication tutorApplication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcceptedByID = new SelectList(db.SSCISUser, "ID", "Login", tutorApplication.AcceptedByID);
            ViewBag.UserID = new SelectList(db.SSCISUser, "ID", "Login", tutorApplication.UserID);
            return View(tutorApplication);
        }

        // GET: TutorApplications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorApplication tutorApplication = db.TutorApplication.Find(id);
            if (tutorApplication == null)
            {
                return HttpNotFound();
            }
            return View(tutorApplication);
        }

        // POST: TutorApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TutorApplication tutorApplication = db.TutorApplication.Find(id);
            db.TutorApplication.Remove(tutorApplication);
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
