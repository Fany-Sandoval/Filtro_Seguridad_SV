using FiltroSeguridad.Filters;
using FiltroSeguridad.Infraestructure;
using FiltroSeguridad.Models;
using FiltroSeguridad.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FiltroSeguridad.Controllers
{
    public class CursosController : Controller
    {
        private FiltrosSeguridadEntities db = new FiltrosSeguridadEntities();
        // GET: Cursos
        public ActionResult Index()
        {
            var cursos = db.Cursos.Include(c => c.Categorias).Include(c => c.UsuariosInscritos).Include(c => c.Status);
            return View(cursos.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Cursos cursos = db.Cursos.Find(id);
            if (cursos == null)
            {
                return HttpNotFound();
            }

            return View(cursos);
        }

        [HttpGet]
        //[AuthorizeRole(Enumeration.Role.Instructor)]
        public ActionResult Create()
        {
            //IServicesProfesor profesor = new ServiceProfesor();
            //ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre");
            //ViewBag.AutorId = new SelectList(profesor.GetProfesores(), "Id", "Nombre");

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[AuthorizeRole(Enumeration.Role.Instructor)]
       public ActionResult Create([Bind(Include = "Nombre,Descripcion,Precio,ImgUrl,Ranking")] Cursos cursos)
        {
            if (ModelState.IsValid)
            {
                db.Cursos.Add(cursos);
                db.SaveChanges();
                return RedirectToAction("Create");

            }
            //return View(cursos);
            if (ModelState.IsValid)
            {
                cursos.StatusId = (int)Enumeration.Status.Activo;
                db.Cursos.Add(cursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", cursos.CategoriaId);
            //ViewBag.AutorId = new SelectList(db.Persona, "Id", "Nombre", cursos.AutorId);
            //ViewBag.StatusId = new SelectList(db.Status, "Id", "Nombre", cursos.StatusId);
            return View(cursos);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cursos cursos = db.Cursos.Find(id);
            if (cursos == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoriasId = new SelectList(db.Categorias, "Id", "Nombre", cursos.CategoriaId);
            ViewBag.AutorId = new SelectList(db.Persona, "Id", "Nombre", cursos.AutorId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Nombre", cursos.StatusId);
            return View(cursos);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion,Precio,ImgURL,Ranking,CategoryId,StatusId,AutorId")] Cursos cursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", cursos.CategoriaId);
            ViewBag.AutorId = new SelectList(db.Persona, "Id", "Nombre", cursos.AutorId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Nombre", cursos.StatusId);
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateRanking([Bind(Include = "Id,Ranking")] Cursos cursos)
        {
            if (ModelState.IsValid)
            {
                Cursos curso = db.Cursos.Find(cursos.Id);
                curso.Ranking = cursos.Ranking;
                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", cursos.CategoriaId);
            ViewBag.AutorId = new SelectList(db.Persona, "Id", "Nombre", cursos.AutorId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Nombre", cursos.StatusId);
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cursos cursos = db.Cursos.Find(id);
            if (cursos == null)
            {
                return HttpNotFound();
            }
            return View(cursos);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Cursos cursos = db.Cursos.Find(id);
            db.Cursos.Remove(cursos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        //[AuthorizeRole(Enumeration.Role.Alumno)]
        public ActionResult AddCurso()
        {
            if (User.Identity.IsAuthenticated)
            {
                IServicesCursos cursos = new ServicesCursos();
                return View(cursos.Get());
            }

            return RedirectToAction("Login", "Account");
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