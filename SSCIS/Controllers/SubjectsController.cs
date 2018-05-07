using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSCIS.Models;
using SSCIS.Attributes;
using SSCIS.Class;

namespace SSCIS.Controllers
{
    /// <summary>
    /// Subjects management controller
    /// </summary>
    public class SubjectsController : Controller
    {
        /// <summary>
        /// Database context
        /// </summary>
        private SSCISEntities db = new SSCISEntities();

        /// <summary>
        /// List of subjects
        /// </summary>
        /// <returns>View with list of subjects</returns>
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Index()
        {
            return View(db.Subject.ToList());
        }

        /// <summary>
        /// Shows detail of subject
        /// </summary>
        /// <param name="id">Subject ID</param>
        /// <returns>View with detail</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subject.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        /// <summary>
        /// Form for cretation of new subject
        /// </summary>
        /// <returns>View with form</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.Subject.Where(s => s.Lesson != null && s.Lesson.Value).ToList(), "ID", "Code");
            return View();
        }

        /// <summary>
        /// Creates new subject
        /// </summary>
        /// <param name="subject">Subject model</param>
        /// <returns>Redirection to list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Create([Bind(Include = "ID,Code,Name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subject.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        /// <summary>
        /// From for editation of existing subject
        /// </summary>
        /// <param name="id">Subject ID</param>
        /// <returns>View with form for editation</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subject.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(db.Subject.Where(s => s.Lesson != null && s.Lesson.Value).ToList(), "ID", "Code");
            return View(subject);
        }

        /// <summary>
        /// Saves changes from editatoin of existing subject
        /// </summary>
        /// <param name="subject">Subject model</param>
        /// <returns>Redirection to list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Edit([Bind(Include = "ID,Code,Name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        /// <summary>
        /// Dialog for deletion of existing subject
        /// </summary>
        /// <param name="id">Subject ID</param>
        /// <returns>View with dialog</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subject.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        /// <summary>
        /// Removes existing subject
        /// </summary>
        /// <param name="id">Subject ID</param>
        /// <returns>Redirection to list</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subject.Find(id);
            db.Subject.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Disposes controller
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
