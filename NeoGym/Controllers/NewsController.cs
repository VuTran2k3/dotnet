using NeoGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoGym.Controllers
{
    public class NewsController : Controller
    {
        NeoGymDbEntities _db = new NeoGymDbEntities();
        // GET: News
        public ActionResult Index()
        {
            var v = from t in _db.News
                    where t.hide == true
                    orderby t.order
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult Detail(long id) {
            var v = from t in _db.News
                    where t.hide == true
                    && t.Id == id
                    orderby t.order
                    select t;
            return View(v.ToList());

        }
    }
}