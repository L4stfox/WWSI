using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zaginieni.DAL;
using Zaginieni.Models;

namespace Zaginieni.Controllers
{
	public class HomeController : Controller
	{
		private Baza db = new Baza();
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult ListaZaginionych()
		{
			return View(db.Info.ToList());
		}

		[Authorize(Roles = "Admin,User")]
		public ActionResult DodajZaginionego()
		{
			return View();
		}

		[Authorize(Roles = "Admin,User")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DodajZaginionego([Bind(Include = "Id,Imie,Nazwisko,Płeć,Informacja,Portret")] Info info)
		{
			if (ModelState.IsValid)
			{
				db.Info.Add(info);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(info);
		}

        // GET: Infoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Info.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Nazwisko,Płeć,Informacja,Portret")] Info info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(info);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Info.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Info info = db.Info.Find(id);
            db.Info.Remove(info);
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