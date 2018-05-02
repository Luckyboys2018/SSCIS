﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSCIS.Models;
using SSCIS.Models.Meta;
using SSCIS.Class;

namespace SSCIS.Controllers
{
    /// <summary>
    /// Controller for tutors applications UCs
    /// </summary>
    public class TutorApplicationsController : Controller
    {
        /// Database context
        private SSCISEntities db = new SSCISEntities();

        /// <summary>
        /// Index UC - list of tutors applications
        /// </summary>
        /// <returns>List of tutors applications view</returns>
        public ActionResult Index()
        {
            var tutorApplication = db.TutorApplication.Include(t => t.AcceptedBy).Include(t => t.Applicant).Where(t => t.IsAccepted == null || !t.IsAccepted.Value);
            return View(tutorApplication.ToList());
        }

        /// <summary>
        /// Detail of tutor application
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <returns>View with details of application</returns>
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
            List<TutorApplicationSubject> subjects = db.TutorApplicationSubject.Where(s => s.ApplicationID == tutorApplication.ID).Include(a => a.Subject).ToList();
            model.ApplicationSubjects = subjects;
            return View(model);
        }

        /// <summary>
        /// Creation of Tutor Application
        /// </summary>
        /// <param name="count">Count of subjects</param>
        /// <returns>View with form</returns>
        public ActionResult Create(int count = 1)
        {
            if (Session["role"] == null) return View("Create_public");

            int? userID = (int)Session["userID"];
            if (db.TutorApplication.Where(a => a.UserID == userID).Count() > 0)
            {
                return RedirectToAction("ApplicationPending");
            }

            MetaTutorApplication model = new MetaTutorApplication();
            for (int i = 0; i < count; i++)
            {
                model.ApplicationSubjects.Add(new TutorApplicationSubject());
            }

            List<TutorApplicationSubject> subjectsList = new List<TutorApplicationSubject>();
            for (int i = 0; i < count; i++)
            {
                subjectsList.Add(new TutorApplicationSubject());
            }
            model.Application.ApplicationSubjects = subjectsList;
            model.CountOfSubjects = count;

            SSCISUser user = db.SSCISUser.Find(userID);
            model.Application.Applicant = user;
            ViewBag.SubjectID = new SelectList(db.Subject.Where(s => s.Lesson != null && !s.Lesson.Value).ToList(), "ID", "Code");
            ViewBag.AcceptedByID = new SelectList(db.SSCISUser, "ID", "Login");
            ViewBag.UserID = new SelectList(db.SSCISUser, "ID", "Login");
            int?[] degrees = new int?[] { 0, 1, 2, 3, 4 };
            ViewBag.Degree = new SelectList(degrees);
            ViewBag.NextCount = count + 1;
            return View(model);
        }

        /// <summary>
        /// Creation of Tutor application
        /// </summary>
        /// <param name="model">Application model</param>
        /// <returns>Result of creation</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MetaTutorApplication model)
        {
            if (ModelState.IsValid)
            {
                model.Application.IsAccepted = false;
                model.Application.ApplicationDate = DateTime.Now;
                int? userID = (int)Session["userID"];
                model.Application.Applicant = db.SSCISUser.Find(userID);
                db.TutorApplication.Add(model.Application);
                db.SaveChanges();

                foreach (TutorApplicationSubject subject in new ApplicationSubjectsResolver().ResolveSubjects(model.SubjectData, db))
                {
                    subject.Application = model.Application;
                    db.TutorApplicationSubject.Add(subject);
                    db.SaveChanges();
                }

                return RedirectToAction("Applied");
            }

            ViewBag.AcceptedByID = new SelectList(db.SSCISUser, "ID", "Login", model.Application.AcceptedByID);
            ViewBag.UserID = new SelectList(db.SSCISUser, "ID", "Login", model.Application.UserID);
            return View(model.Application);
        }

        /// <summary>
        /// Acceptation of pendind tutor application
        /// </summary>
        /// <param name="id">Applications ID</param>
        /// <returns>View</returns>
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
            int? userID = (int)Session["userID"];
            tutorApplication.AcceptedDate = DateTime.Now;
            tutorApplication.IsAccepted = true;
            tutorApplication.AcceptedBy = db.SSCISUser.Find(userID);
            db.SaveChanges();

            tutorApplication.Applicant.Role = db.Role.Where(r => "TUTOR".Equals(r.RoleCode)).Single();
            db.SaveChanges();

            List<TutorApplicationSubject> subjects = db.TutorApplicationSubject.Where(s => s.ApplicationID == id).ToList();
            List<Subject> parents = new List<Subject>();
            foreach (TutorApplicationSubject subject in subjects)
            {
                if (subject.Subject.Parent != null && !parents.Contains(subject.Subject.Parent))
                {
                    parents.Add(subject.Subject.Parent);
                }
            }
            foreach (Subject subject in parents)
            {
                Approval app = new Approval();
                app.Tutor = tutorApplication.Applicant;
                app.Subject = subject;
                db.Approval.Add(app);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Declines tutor application
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <returns>View</returns>
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

        /// <summary>
        /// Shows view with message of just created application
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Applied()
        {
            return View();
        }

        /// <summary>
        /// Shows view with message for pending application
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult ApplicationPending()
        {
            return View();
        }

        /// <summary>
        /// Disposes controller
        /// </summary>
        /// <param name="disposing">disposing db value</param>
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
