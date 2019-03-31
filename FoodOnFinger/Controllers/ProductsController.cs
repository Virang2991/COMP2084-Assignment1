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
    public class ProductsController : Controller
    {
        //private ProductView db = new ProductView();
        IMockProducts db;

        //Const

        public ProductsController()

        {

            this.db = new IDataProducts();

        }

        public ProductsController(IMockProducts mockdb)

        {

            this.db = mockdb;

        }

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Cuisine);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.SingleOrDefault(m => m.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Description,CuisineID,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Save(product);
                return RedirectToAction("Index");
            }

            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "Name", product.CuisineID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.SingleOrDefault(m => m.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "Name", product.CuisineID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Description,CuisineID,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Save(product);
                return RedirectToAction("Index");
            }
            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "Name", product.CuisineID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.SingleOrDefault(m => m.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.SingleOrDefault(m => m.ProductID == id);
            db.Delete(product);
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
