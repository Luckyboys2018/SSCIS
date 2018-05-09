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
    /// Static contents controller
    /// </summary>
    public class ContentsController : Controller
    {
        /// <summary>
        /// Database context
        /// </summary>
        private SSCISEntities db = new SSCISEntities();

        /// <summary>
        /// Content detail
        /// </summary>
        /// <param name="id">content id</param>
        /// <returns>View with content detail</returns>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISContent sSCISContent = db.SSCISContent.Find(id);
            if (sSCISContent == null)
            {
                return HttpNotFound();
            }
            return View(sSCISContent);
        }

        /// <summary>
        /// Creates new content
        /// </summary>
        /// <returns>View with form</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Create()
        {
            SSCISContent model = new SSCISContent();
            return View(model);
        }

        /// <summary>
        /// Creates new content and saves it to db
        /// </summary>
        /// <param name="sSCISContent">Content model</param>
        /// <returns>Redirection to List</returns>
        [HttpPost]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Create(SSCISContent model)
        {
            if (ModelState.IsValid)
            {
                model.Created = DateTime.Now;
                model.Author = db.SSCISUser.Find((int)Session["userID"]);
                db.SSCISContent.Add(model);
                db.SaveChanges();
                return RedirectToAction("News", "Home");
            }
            return View(model);
        }

        /// <summary>
        /// Editation of existing content
        /// </summary>
        /// <param name="id">Content ID</param>
        /// <returns>Editation form view</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISContent sSCISContent = db.SSCISContent.Find(id);
            if (sSCISContent == null)
            {
                return HttpNotFound();
            }
            return View(sSCISContent);
        }

        /// <summary>
        /// Editation of existing content - saves changes
        /// </summary>
        /// <param name="sSCISContent">Content model</param>
        /// <returns>Redirection to list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Edit([Bind(Include = "ID,AuthorID,EditedByID,Created,Edited,TextContent")] SSCISContent sSCISContent)
        {
            if (ModelState.IsValid)
            {
                sSCISContent.Edited = DateTime.Now;
                sSCISContent.EditedBy = db.SSCISUser.Find((int)Session["userID"]);
                db.SaveChanges();
                return RedirectToAction("News", "Home");
            }
            return View(sSCISContent);
        }

        /// <summary>
        /// Deletion dialog
        /// </summary>
        /// <param name="id">Content id</param>
        /// <returns>View with deletion digalog</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISContent sSCISContent = db.SSCISContent.Find(id);
            if (sSCISContent == null)
            {
                return HttpNotFound();
            }
            return View(sSCISContent);
        }

        /// <summary>
        /// Delete existing content
        /// </summary>
        /// <param name="id">Content id</param>
        /// <returns>Redirectio to list</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult DeleteConfirmed(int id)
        {
            SSCISContent sSCISContent = db.SSCISContent.Find(id);
            db.SSCISContent.Remove(sSCISContent);
            db.SaveChanges();
            return RedirectToAction("News", "Home");
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
