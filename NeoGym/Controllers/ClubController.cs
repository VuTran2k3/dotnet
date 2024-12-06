using NeoGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoGym.Controllers
{
    public class ClubController : Controller
    {
        NeoGymDbEntities _db = new NeoGymDbEntities();
        // GET: Club
        public ActionResult Index()
        {
            var v = from t in _db.Club
                    where t.hide == true
                    orderby t.order
                    select t;
            return PartialView(v.ToList());
        }
    }
}