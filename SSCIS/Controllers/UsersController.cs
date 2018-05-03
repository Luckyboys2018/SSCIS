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
    /// Users management controller
    /// </summary>
    public class UsersController : Controller
    {
        /// <summary>
        /// Database context
        /// </summary>
        private SSCISEntities db = new SSCISEntities();

        /// <summary>
        /// Users list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Index()
        {
            var sSCISUser = db.SSCISUser.Include(s => s.Role).Include(s => s.ActivatedBy);
            return View(sSCISUser.ToList());
        }

        /// <summary>
        /// Users detail
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>View with user detail</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISUser sSCISUser = db.SSCISUser.Find(id);
            if (sSCISUser == null)
            {
                return HttpNotFound();
            }
            return View(sSCISUser);
        }

        /// <summary>
        /// Lgged users detail
        /// </summary>
        /// <returns>View with logged user detail</returns>
        [HttpGet]
        public ActionResult Profil()
        {
            int? id = (int)Session["userID"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISUser sSCISUser = db.SSCISUser.Find(id);
            if (sSCISUser == null)
            {
                return HttpNotFound();
            }
            return View("Detail", sSCISUser);
        }

        /// <summary>
        /// Editation of existing user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>View with form for editation</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISUser sSCISUser = db.SSCISUser.Find(id);
            if (sSCISUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Role, "ID", "RoleCode", sSCISUser.RoleID);
            ViewBag.ActivatedByID = new SelectList(db.SSCISUser, "ID", "Login", sSCISUser.ActivatedByID);
            return View(sSCISUser);
        }

        /// <summary>
        /// Saves changes from editation of user
        /// </summary>
        /// <param name="sSCISUser">User model</param>
        /// <returns>Redirection to list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Edit([Bind(Include = "ID,Login,Firstname,Lastname,Password,RoleID,IsActive,Created,Activated,ActivatedByID,StudentNumber")] SSCISUser sSCISUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sSCISUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Role, "ID", "RoleCode", sSCISUser.RoleID);
            ViewBag.ActivatedByID = new SelectList(db.SSCISUser, "ID", "Login", sSCISUser.ActivatedByID);
            return View(sSCISUser);
        }

        /// <summary>
        /// Deletion dialog of existing user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Dialog view</returns>
        [HttpGet]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSCISUser sSCISUser = db.SSCISUser.Find(id);
            if (sSCISUser == null)
            {
                return HttpNotFound();
            }
            return View(sSCISUser);
        }

        /// <summary>
        /// Deletes existing user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Redirection to list</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SSCISAuthorize(AccessLevel = AuthorizationRoles.Administrator)]
        public ActionResult DeleteConfirmed(int id)
        {
            SSCISUser sSCISUser = db.SSCISUser.Find(id);
            db.SSCISUser.Remove(sSCISUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Disposes controller
        /// </summary>
        /// <param name="disposing">Dispose database context</param>
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
