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
    /// Controller for Parameters management
    /// </summary>
    public class SSCISParamsController : Controller
    {
        /// <summary>
        /// Database context
        /// </summary>
        private SSCISEntities db = new SSCISEntities();

        /// <summary>
        /// List of params
        /// </summary>
        /// <returns>View with all params</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Index()
        {
            return View(db.SSCISParam.ToList());
        }

        /// <summary>
        /// Detail of system parameter
        /// </summary>
        /// <param name="id">Parameter ID</param>
        /// <returns>View with detail</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISParam sSCISParam = db.SSCISParam.Find(id);
            if (sSCISParam == null)
            {
                return HttpNotFound();
            }
            return View(sSCISParam);
        }

        /// <summary>
        /// Form for creation of new parameter
        /// </summary>
        /// <returns>View with form</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates new system parameter
        /// </summary>
        /// <param name="sSCISParam">Parameter model</param>
        /// <returns>Redirection to list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Create([Bind(Include = "ID,ParamKey,ParamValue,Description")] SSCISParam sSCISParam)
        {
            if (ModelState.IsValid)
            {
                db.SSCISParam.Add(sSCISParam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sSCISParam);
        }

        /// <summary>
        /// Form for editing existing parameter
        /// </summary>
        /// <param name="id">Parameter ID</param>
        /// <returns>View with form</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISParam sSCISParam = db.SSCISParam.Find(id);
            if (sSCISParam == null)
            {
                return HttpNotFound();
            }
            return View(sSCISParam);
        }

        /// <summary>
        /// Saves changes from editatoin of parameter
        /// </summary>
        /// <param name="sSCISParam">Parameter model</param>
        /// <returns>Redirection to list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Edit([Bind(Include = "ID,ParamKey,ParamValue,Description")] SSCISParam sSCISParam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sSCISParam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sSCISParam);
        }

        /// <summary>
        /// Deletion of existing parameter form
        /// </summary>
        /// <param name="id">Parameter ID</param>
        /// <returns>View with deletion dialogue</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISParam sSCISParam = db.SSCISParam.Find(id);
            if (sSCISParam == null)
            {
                return HttpNotFound();
            }
            return View(sSCISParam);
        }

        /// <summary>
        /// Deletes existing parameter
        /// </summary>
        /// <param name="id">Parameter ID</param>
        /// <returns>Redirection to list</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult DeleteConfirmed(int id)
        {
            SSCISParam sSCISParam = db.SSCISParam.Find(id);
            db.SSCISParam.Remove(sSCISParam);
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
