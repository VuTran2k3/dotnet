using NeoGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoGym.Controllers
{
    public class HomeController : Controller
    {
        NeoGymDbEntities _db = new NeoGymDbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Why()
        {
            return View();
        }

        public ActionResult Trainers()
        {
            return View();
        }
        public ActionResult GetNews()
        {
            var v = from t in _db.News
                    where t.hide == true
                    orderby t.order
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult GetCategory()
        {
            var v = from t in _db.Category
                    where t.hide == true
                    orderby t.order
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult GetClub() {
            var v = from t in _db.Club
                    where t.hide == true
                    orderby t.order
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(Boolean partial = false)
        {
            ViewBag.Message = "Your contact page.";
            if (partial) return PartialView();
            return View();
        }
    }
}