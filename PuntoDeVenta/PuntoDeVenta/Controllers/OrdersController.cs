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
            var orders = db.Orders.Where(x => x.Active == "POR PAGAR").Include(o => o.Product).Include(o => o.Table);
            StaticConfig staticConfig = db.StaticConfig.FirstOrDefault();

            var ids = new List<string>();
            var numbers = new List<int>();
            foreach (var item in orders)
            {
                if (!ids.Contains(item.GroupID))
                {
                    ids.Add(item.GroupID);
                }
                if (!numbers.Contains(item.Table.TableNumber))
                {
                    numbers.Add(item.Table.TableNumber);
                }
            }
            ViewBag.ids = ids;
            ViewBag.numbers = numbers;
            ViewBag.staticConfig = staticConfig;

            return View(orders.ToList());
        }

        public ActionResult Paid()
        {
            var orders = db.Orders.Where(x => x.Active == "PAGADO").Include(o => o.Product).Include(o => o.Table);
            var ids = new List<string>();
            foreach (var item in orders)
            {
                if (!ids.Contains(item.GroupID))
                {
                    ids.Add(item.GroupID);
                }
            }
            ViewBag.ids = ids;

            return View(orders.ToList());
        }

        [HttpPost]
        public ActionResult OrdersPost(List<Order> orders)
        {
            var list = db.Orders.Where(o => o.Active == "POR PAGAR").ToList();
            list = list.Where(o => o.TableId == orders.First().TableId).ToList();
            if (list.Count == 0)
            {
                var date = DateTime.Now;
                var active = "POR PAGAR";
                foreach (var item in orders)
                {
                    item.Active = active;
                    item.Date = date;
                    item.GroupID = (date.ToString() + item.TableId.ToString()).Replace("/", null).Replace(" ", null).Replace(":", null).Replace(".", null);
                    db.Orders.Add(item);
                }
            }
            else
            {
                var id = list.First().GroupID;

                var date = DateTime.Now;
                var active = "POR PAGAR";
                foreach (var item in orders)
                {
                    item.Active = active;
                    item.Date = date;
                    item.GroupID = id;
                    db.Orders.Add(item);
                }
            }

            try
            {
                db.SaveChanges();
                return Json(new { ok = true, newurl = Url.Action("List") });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex });

            }
        }

        [HttpPost]
        public ActionResult Pay(string id)
        {
            var orders = db.Orders.Where(o => o.GroupID == id);
            orders = orders.Where(o => o.Active == "POR PAGAR");
            foreach (var order in orders)
            {
                order.Active = "PAGADO";
                db.Entry(order).State = EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
                return Json(new { ok = true, newurl = Url.Action("List") });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex });

            }
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.categories = db.Categories.ToList().OrderBy(c => c.CategoryName);
            ViewBag.products = db.Products.ToList();
            ViewBag.tables = db.Tables.ToList().OrderBy(a => a.TableNumber);
            var busy = new List<int>();
            foreach (var item in db.Orders.Where(x => x.Active == "POR PAGAR").Include(o => o.Table))
            {
                if (!busy.Contains(item.Table.TableNumber))
                {
                    busy.Add(item.Table.TableNumber);
                }
            }
            ViewBag.busy = busy;

            return View();
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
