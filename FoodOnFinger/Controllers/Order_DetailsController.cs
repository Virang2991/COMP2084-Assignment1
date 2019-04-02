using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodOnFinger.Models;

namespace FoodOnFinger.Controllers
{
    public class Order_DetailsController : Controller
    {
        private ProductView db = new ProductView();

        // GET: Order_Details
        public ActionResult Index()
        {
            var order_Details = db.Order_Details.Include(o => o.Cuisine).Include(o => o.Product);
            return View(order_Details.ToList());
        }

        // GET: Order_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            return View(order_Details);
        }

        // GET: Order_Details/Create
        public ActionResult Create()
        {
            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: Order_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CuisineID,Address,ProductID,Date,Contact,Total")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                db.Order_Details.Add(order_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "Name", order_Details.CuisineID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order_Details.ProductID);
            return View(order_Details);
        }

        // GET: Order_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "Name", order_Details.CuisineID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order_Details.ProductID);
            return View(order_Details);
        }

        // POST: Order_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CuisineID,Address,ProductID,Date,Contact,Total")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "Name", order_Details.CuisineID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order_Details.ProductID);
            return View(order_Details);
        }

        // GET: Order_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            return View(order_Details);
        }

        // POST: Order_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Details order_Details = db.Order_Details.Find(id);
            db.Order_Details.Remove(order_Details);
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
