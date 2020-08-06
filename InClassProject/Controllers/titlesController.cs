using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using InClassProject;

namespace InClassProject.Controllers
{   [Authorize]
    public class titlesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View(new employee());

        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(employee employee)
        {

          employee  employee1 = db.employees.Find(employee.emp_id);
            if (employee1.emp_id != null)
            {
                if (employee1.fname.Equals(employee.fname))
                {
                 
                   //FormsAuthentication.SetAuthCookie(employee1.emp_id, true);
                   return RedirectToAction("Index", "titles");
                }
            }
            ViewBag.msg = "Invalid";
            return View();
        }
        // GET: titles
        public ActionResult Index()
        {
            return View(db.titles.ToList());
        }

        // GET: titles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<titleauthor> titleauthors = db.titleauthors.Where(ta => ta.title_id == id).ToList();
            
            if (titleauthors == null)
            {
                return HttpNotFound();
            }


            return View(titleauthors);
        }

        // GET: titles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: titles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,title1,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] title title)
        {
            if (ModelState.IsValid)
            {
                db.titles.Add(title);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(title);
        }

        // GET: titles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            title title = db.titles.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: titles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,title1,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] title title)
        {
            if (ModelState.IsValid)
            {
                db.Entry(title).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(title);
        }

        // GET: titles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            title title = db.titles.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            title title = db.titles.Find(id);
            db.titles.Remove(title);
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
