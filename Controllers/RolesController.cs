using FiltroSeguridad.Filters;
using FiltroSeguridad.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FiltroSeguridad.Controllers
{
    public class RolesController : Controller
    {
        private FiltrosSeguridadEntities db = new FiltrosSeguridadEntities();

        // GET: Roles
        public ActionResult Index()
        {
            var roles = db.Role.Include(c => c.Usuario).ToList();
            return View(roles);
        }

        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Role role = db.Role.Find(id);
            db.Role.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Nombre,Descripcion")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Role.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}