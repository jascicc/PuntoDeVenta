using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PuntoDeVenta.Models;

namespace PuntoDeVenta.Controllers
{
    public class SpendingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Spendings
        public ActionResult Index()
        {
            return View(db.Spendings.ToList());
        }

        // GET: Spendings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Spendings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpendingID,Description,Cost,Date")] Spending spending)
        {
            if (ModelState.IsValid)
            {
                db.Spendings.Add(spending);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spending);
        }

        // GET: Spendings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spending spending = db.Spendings.Find(id);
            if (spending == null)
            {
                return HttpNotFound();
            }
            return View(spending);
        }

        // POST: Spendings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpendingID,Description,Cost,Date")] Spending spending)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spending).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spending);
        }

        // GET: Spendings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spending spending = db.Spendings.Find(id);
            if (spending == null)
            {
                return HttpNotFound();
            }
            return View(spending);
        }

        // POST: Spendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spending spending = db.Spendings.Find(id);
            db.Spendings.Remove(spending);
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
