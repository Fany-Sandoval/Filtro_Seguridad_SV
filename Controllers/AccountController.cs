using FiltroSeguridad.Infraestructure;
using FiltroSeguridad.Models;
using FiltroSeguridad.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FiltroSeguridad.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registro()
        {
            return View("Registro");
        }

        [HttpPost]
        public ActionResult Login(Usuario model)
        {
            if (ModelState.IsValid)
            {
                IServiceAccount accountService = new ServicesAccount();
                Usuario user = accountService.Login(model);
                if(user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.NombreUsuario, false);
                    FormsAuthentication.SetAuthCookie(Convert.ToString(user.id), true);

                    var authTicket = new FormsAuthenticationTicket(1, user.NombreUsuario, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Role.Nombre);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario no encontrado");
                }
            }
            return View();
        }

        [HttpGet] 
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AccesoDenegado()
        {
            return View("Forbidden");
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult AtaqueXss(string nombre = "prueba")
        {
            ViewBag.Nombre = nombre;
            return View("AtaqueXss");
        }
    }
}