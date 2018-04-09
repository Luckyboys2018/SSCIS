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
    public class ContentsController : Controller
    {
        private SSCISEntities db = new SSCISEntities();

        // GET: Contents
        //public ActionResult Index()
        //{
        //    var sSCISContent = db.SSCISContent.Include(s => s.Author).Include(s => s.EditedBy);
        //    return View(sSCISContent.ToList());
        //}

        // GET: Contents/Details/5
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

        // GET: Contents/Create
        public ActionResult Create()
        {
            SSCISContent model = new SSCISContent();
            model.Author = db.SSCISUser.Find((int)Session["userID"]);
            return View(model);
        }

        // POST: Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AuthorID,EditedByID,Created,Edited,TextContent")] SSCISContent sSCISContent)
        {
            if (ModelState.IsValid)
            {
                sSCISContent.Created = DateTime.Now;
                db.SSCISContent.Add(sSCISContent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.SSCISUser, "ID", "Login", sSCISContent.AuthorID);
            ViewBag.EditedByID = new SelectList(db.SSCISUser, "ID", "Login", sSCISContent.EditedByID);
            return View(sSCISContent);
        }

        // GET: Contents/Edit/5
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
            ViewBag.AuthorID = new SelectList(db.SSCISUser, "ID", "Login", sSCISContent.AuthorID);
            ViewBag.EditedByID = new SelectList(db.SSCISUser, "ID", "Login", sSCISContent.EditedByID);
            return View(sSCISContent);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AuthorID,EditedByID,Created,Edited,TextContent")] SSCISContent sSCISContent)
        {
            if (ModelState.IsValid)
            {
                sSCISContent.Edited = DateTime.Now;
                sSCISContent.EditedBy = db.SSCISUser.Find((int)Session["userID"]);
                db.Entry(sSCISContent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.SSCISUser, "ID", "Login", sSCISContent.AuthorID);
            ViewBag.EditedByID = new SelectList(db.SSCISUser, "ID", "Login", sSCISContent.EditedByID);
            return View(sSCISContent);
        }

        // GET: Contents/Delete/5
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

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SSCISContent sSCISContent = db.SSCISContent.Find(id);
            db.SSCISContent.Remove(sSCISContent);
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
