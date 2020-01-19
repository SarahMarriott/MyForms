using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyFormBuilder.Models;

namespace MyFormBuilder.Controllers
{
    public class MyFormSubmissionsController : Controller
    {
        private MyFormBuilderModel db = new MyFormBuilderModel();

        // GET: MyFormSubmissions
        public ActionResult Index()
        {
            return View(db.MyFormSubmissions.ToList());
        }

        // GET: MyFormSubmissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyFormSubmission myFormSubmission = db.MyFormSubmissions.Find(id);
            if (myFormSubmission == null)
            {
                return HttpNotFound();
            }
            return View(myFormSubmission);
        }

        // GET: MyFormSubmissions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyFormSubmissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ApplicationUserID,DateTimeCreated,SubmittedData")] MyFormSubmission myFormSubmission)
        {
            if (ModelState.IsValid)
            {
                db.MyFormSubmissions.Add(myFormSubmission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myFormSubmission);
        }

        // GET: MyFormSubmissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyFormSubmission myFormSubmission = db.MyFormSubmissions.Find(id);
            if (myFormSubmission == null)
            {
                return HttpNotFound();
            }
            return View(myFormSubmission);
        }

        // POST: MyFormSubmissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ApplicationUserID,DateTimeCreated,SubmittedData")] MyFormSubmission myFormSubmission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myFormSubmission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myFormSubmission);
        }

        // GET: MyFormSubmissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyFormSubmission myFormSubmission = db.MyFormSubmissions.Find(id);
            if (myFormSubmission == null)
            {
                return HttpNotFound();
            }
            return View(myFormSubmission);
        }

        // POST: MyFormSubmissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyFormSubmission myFormSubmission = db.MyFormSubmissions.Find(id);
            db.MyFormSubmissions.Remove(myFormSubmission);
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
