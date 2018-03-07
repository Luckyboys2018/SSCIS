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

namespace SSCIS.Controllers
{
    public class SSCISParamsController : Controller
    {
        private SSCISEntities db = new SSCISEntities();

        // GET: SSCISParams
        [SSCISAuthorize(AccessLevel = "ADMIN")]
        public ActionResult Index()
        {
            return View(db.SSCISParam.ToList());
        }

        // GET: SSCISParams/Details/5
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

        // GET: SSCISParams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SSCISParams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: SSCISParams/Edit/5
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

        // POST: SSCISParams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: SSCISParams/Delete/5
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

        // POST: SSCISParams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SSCISParam sSCISParam = db.SSCISParam.Find(id);
            db.SSCISParam.Remove(sSCISParam);
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
