using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NeoGym.Models;
using ShopOnline.Help;

namespace NeoGym.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private NeoGymDbEntities db = new NeoGymDbEntities();

        // GET: Admin/Product
        public ActionResult Index(long? selectedId = null )
        {
            getCategory(selectedId);
            return View();
        }
        public ActionResult GetProduct(long? id = null)
        {
            if (id == null)
            {
                var v = db.Product.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.Product.Where(x => x.categoryId == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }

        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.Category.Where(x => x.hide == true)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.Category, "id", "name");
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.Category, "id", "name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,link,description,image,price,categoryId,meta,hide,order,datebegin")] Product product, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    //filename = Guid.NewGuid().ToString() + img.FileName;
                    filename = img.FileName;
                    //Debug.WriteLine("file exists: "+ filename);
                    path = Path.Combine(Server.MapPath("~/Content/images/product/"), filename);
                    img.SaveAs(path);
                    product.image = filename; //Lưu ý
                }
                else
                {
                    product.image = "u-1.png";
                }
                product.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                product.order = getMaxOrder(product.categoryId);
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            product.order = getMaxOrder(product.categoryId);
            product.description += " filename";
            ViewBag.categoryId = new SelectList(db.Category, "id", "name", product.categoryId);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.Category, "id", "name", product.categoryId);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,description,image,price,categoryId,meta,hide,order,datebegin")] Product product, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            Product temp = db.Product.Find(product.id);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    //filename = Guid.NewGuid().ToString() + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images/product"), filename);
                    img.SaveAs(path);
                    temp.image = filename; //Lưu ý
                }
                temp.name = product.name;
                temp.price = product.price;
                temp.description = product.description; //convert Tiếng Việt không dấu
                temp.hide = product.hide;
                temp.order = product.order;
                temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(db.Category, "id", "name", product.categoryId);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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

        public int getMaxOrder(long? CategoryId)
        {
            if (CategoryId == null)
                return 1;
            return db.Product.Where(x => x.categoryId == CategoryId).Count();
        }
    }
}
