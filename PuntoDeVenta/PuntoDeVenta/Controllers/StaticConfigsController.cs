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
    public class StaticConfigsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StaticConfigs/Details
        public ActionResult Details()
        {
            StaticConfig staticConfig = db.StaticConfig.FirstOrDefault();
            if (staticConfig == null)
            {
                return HttpNotFound();
            }
            return View(staticConfig);
        }

        // GET: StaticConfigs/Edit
        public ActionResult Edit()
        {
            StaticConfig staticConfig = db.StaticConfig.FirstOrDefault();
            if (staticConfig == null)
            {
                return HttpNotFound();
            }
            return View(staticConfig);
        }

        // POST: StaticConfigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Dollar,Name,RFC,Address,Phone")] StaticConfig staticConfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staticConfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(staticConfig);
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
