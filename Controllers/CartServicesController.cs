using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeOnUs.Models;

namespace HomeOnUs.Controllers
{
    public class CartServicesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: CartServices
        public ActionResult Index()
        {
            var cartServices = db.CartServices.Include(c => c.Cart).Include(c => c.Company);
            return View(cartServices.ToList());
        }

        // GET: CartServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartService cartService = db.CartServices.Find(id);
            if (cartService == null)
            {
                return HttpNotFound();
            }
            return View(cartService);
        }

        // GET: CartServices/Create
        public ActionResult Create()
        {
            ViewBag.CartID = new SelectList(db.Carts, "Id", "Status");
            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "CompanyName");
            return View();
        }

        // POST: CartServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantity,CartID,CompanyID")] CartService cartService)
        {
            if (ModelState.IsValid)
            {
                db.CartServices.Add(cartService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartID = new SelectList(db.Carts, "Id", "Status", cartService.CartID);
            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "CompanyName", cartService.CompanyID);
            return View(cartService);
        }

        // GET: CartServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartService cartService = db.CartServices.Find(id);
            if (cartService == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartID = new SelectList(db.Carts, "Id", "Status", cartService.CartID);
            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "CompanyName", cartService.CompanyID);
            return View(cartService);
        }

        // POST: CartServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity,CartID,CompanyID")] CartService cartService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartID = new SelectList(db.Carts, "Id", "Status", cartService.CartID);
            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "CompanyName", cartService.CompanyID);
            return View(cartService);
        }

        // GET: CartServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartService cartService = db.CartServices.Find(id);
            if (cartService == null)
            {
                return HttpNotFound();
            }
            return View(cartService);
        }

        // POST: CartServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartService cartService = db.CartServices.Find(id);
            db.CartServices.Remove(cartService);
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
