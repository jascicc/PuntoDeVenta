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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Product).Include(o => o.Table);
            return View(orders.ToList());
        }

        public ActionResult List()
        {
            var orders = db.Orders.Where(x => x.Active == "por pagar").Include(o => o.Product).Include(o => o.Table);
            var dis = new List<int>();
            foreach (var item in orders)
            {
                if (!dis.Contains(item.Table.TableNumber))
                {
                    dis.Add(item.Table.TableNumber);
                }
            }
            ViewBag.dis = dis;

            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.categories = db.Categories.ToList();
            ViewBag.products = db.Products.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,TableId,ProductId,Quantity,Date,Active")] Order order)
        {
            order.Active = "por pagar";
            if (ModelState.IsValid)
            {
                order.Date = DateTime.Now;
                //db.Orders.Add(order);
                //db.SaveChanges();
                //return RedirectToAction("List");
            }

            ViewBag.categories = db.Categories.ToList();
            ViewBag.products = db.Products.ToList();
            return View(order);
        }

        //GET: Orders/Create1
        public ActionResult Create1()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.TableId = new SelectList(db.Tables, "TableId", "TableNumber");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "OrderId,TableId,ProductId,Quantity,Date,Active")] Order order)
        {
            order.Active = "por pagar";
            if (ModelState.IsValid)
            {
                order.Date = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.TableId = new SelectList(db.Tables, "TableId", "TableNumber", order.TableId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", order.ProductID);
            ViewBag.TableId = new SelectList(db.Tables, "TableId", "Name", order.TableId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,TableId,ProductId,Quantity,Date")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", order.ProductID);
            ViewBag.TableId = new SelectList(db.Tables, "TableId", "Name", order.TableId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
