using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NeoGym.Models;

namespace NeoGym.Controllers
{
    public class TempController : Controller
    {
        NeoGymDbEntities _db = new NeoGymDbEntities();
        // GET: Temp
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getMenu()
        {
            var v = from t in _db.menu
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
    }
}