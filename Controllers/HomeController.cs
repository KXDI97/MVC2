using MVC2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC2.Services;
using MVC2.Security;
using MVC2.Context;

namespace MVC2.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebMVC2Context db = new WebMVC2Context();
        private readonly PasswordEncripter encripter = new PasswordEncripter();
        private readonly IAuthorizationService _authService = new AuthorizationService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["AlertMessage"] = "Datos de inicio de sesión inválidos.";
                return RedirectToAction("Index");
            }

            // Usamos el servicio de autorización para validar al usuario
            AuthResults result = _authService.Auth(model.Username, model.Password, out User user);

            switch (result)
            {
                case AuthResults.Success:
                    // Si es exitoso, actualizamos la cookie y redirigimos
                    CookieUpdate(user);
                    return RedirectToAction("Index", "Home");

                case AuthResults.PasswordNotMatch:
                    TempData["AlertMessage"] = "La contraseña es incorrecta.";
                    break;

                case AuthResults.NotExists:
                    TempData["AlertMessage"] = "El usuario no existe.";
                    break;

                case AuthResults.Error:
                default:
                    TempData["AlertMessage"] = "Ocurrió un error al procesar la solicitud.";
                    break;
            }

            // Redirigir al inicio en caso de error
            return RedirectToAction("Index");

            
        }

        public ActionResult LoginPartial()
        {
            return PartialView("LoginPartial");
        }

        
        public ActionResult LogoutPartial()
        {
            return PartialView("LogoutPartial");
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0)]
        [ValidateAntiForgeryToken]
        public ActionResult RefreshLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Message = "" }, JsonRequestBehavior.AllowGet);
            }
            User users = new User(); ;
            var result = _authService.Auth(model.Username, model.Password, out User user);
            switch (result)
            {
                case AuthResults.Success:
                    CookieUpdate(user);
                    return Json(new { Message = "Cookies Refrescados Correctamente." }, JsonRequestBehavior.AllowGet);
                case AuthResults.PasswordNotMatch:
                    return Json(new { Message = "La contraseña no es valida." }, JsonRequestBehavior.AllowGet);
                case AuthResults.NotExists:
                    return Json(new { Message = "El usuario no es valido." }, JsonRequestBehavior.AllowGet);
                default:
                    return Json(new { Message = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        private void CookieUpdate(User user)
        {
            var ticket = new FormsAuthenticationTicket(2,
                user.Username,
                DateTime.Now,
                DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                false,
                JsonConvert.SerializeObject(user)
            );
            Session["Username"] = user.Username;
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)) { };
            Response.AppendCookie(cookie);
        }

        public AuthResults Auth(string username, string password, out User user)
        {
            user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return AuthResults.NotExists;
            }

            // Verifica si la contraseña coincide
            var decryptedPassword = encripter.Decrypt(user.Password, new List<byte[]> { user.HashKey, user.HashIV });
            if (decryptedPassword == password)
            {
                return AuthResults.Success;
            }

            return AuthResults.PasswordNotMatch;
        }
    }
}
