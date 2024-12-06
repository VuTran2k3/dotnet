using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NeoGym.Models;

namespace NeoGym.Areas.Admin.Controllers
{
    public class ClubsController : Controller
    {
        private NeoGymDbEntities db = new NeoGymDbEntities();

        // GET: Admin/Clubs
        public ActionResult Index()
        {
            return View(db.Club.ToList());
        }

        // GET: Admin/Clubs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Club.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // GET: Admin/Clubs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Clubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,link,description,image,price,meta,hide,order,datebegin")] Club club, HttpPostedFileBase img)
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
                    path = Path.Combine(Server.MapPath("~/Content/images/club"), filename);
                    img.SaveAs(path);
                    club.image = filename; //Lưu ý
                }
                club.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Club.Add(club);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(club);
        }

        // GET: Admin/Clubs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Club.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // POST: Admin/Clubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,description,image,price,meta,hide,order,datebegin")] Club club, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            Product temp = db.Product.Find(club.id);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    //filename = Guid.NewGuid().ToString() + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images/club"), filename);
                    img.SaveAs(path);
                    temp.image = filename; //Lưu ý
                }
                temp.name = club.name;
                temp.link = club.link;
                temp.meta = club.meta;
                temp.price = club.price;
                temp.description = club.description; //convert Tiếng Việt không dấu
                temp.hide = club.hide;
                temp.order = club.order;
                temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(club);
        }

        // GET: Admin/Clubs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Club.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // POST: Admin/Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Club club = db.Club.Find(id);
            db.Club.Remove(club);
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
