using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebVendaDireta.Models;

namespace WebVendaDireta.Controllers
{
    public class UsuarioController : Controller
    {
        private Context db = new Context();

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Usuário - Criar";
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "UsuarioId,Nome,Email,Senha")] Usuario u)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(u);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Title = "Usuário - Login";
            return View();
        }

        [HttpPost]
        public ActionResult Login_Antigo(FormCollection form)
        {
            var u = db.Usuarios
                .Where(t => t.Email == form["Email"] && t.Senha == form["Senha"]).FirstOrDefault();

            if (u != null)
            {
                Session["usuario"] = u;
                return RedirectToAction("Index", "Produto");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var result = false;
            var senha = form["Senha"];
            var email = form["Email"];
            var o = from c in db.Usuarios
                    where c.Email == email && c.Senha == senha
                    select c;

            if (o.ToList().Count > 0)
                result = true;

            if (result)
            {
                Session["usuario"] = o.FirstOrDefault();
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                ModelState.AddModelError("", "Login ou senha inválidos!");
                TempData["Mensagem"] = "Login ou senha inválidos!";
                return RedirectToAction("Login", "Usuario");
            }
        }

        public ActionResult Logout()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Usuario");
        }
    }
}
