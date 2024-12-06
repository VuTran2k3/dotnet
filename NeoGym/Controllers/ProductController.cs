using NeoGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoGym.Controllers
{
    public class ProductController : Controller
    {
        NeoGymDbEntities _db = new NeoGymDbEntities();
        
        public ActionResult Index(string meta)
        {
            ViewBag.Title = "Sản Phẩm";
            if (meta == null)
            {
                var category_1 = from t in _db.Category
                                 where t.hide == true
                                 orderby t.order
                                 select t;
                return PartialView(category_1.ToList());
            }
            var category_2 = from t in _db.Category
                             where t.hide == true && t.meta == meta
                             orderby t.order
                             select t;

            return PartialView(category_2.ToList());
        }
        public ActionResult getProduct(long id) {
            var product = from t in _db.Product
                        where t.hide == true
                        && t.categoryId == id
                        select t;
            return PartialView(product.ToList());
        }

        public ActionResult Detail(string meta)
        {
            var product = from t in _db.Product
                          where t.hide == true
                          && t.meta == meta
                          select t;
            if (product == null)
            {
                return HttpNotFound(); // or redirect to an error page
            }
            return View(product.FirstOrDefault());
        }
    }
}
