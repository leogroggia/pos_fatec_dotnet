using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using WebVendaDireta.Models;
using System.Net;

namespace WebVendaDireta.Controllers
{
    public class ProdutoController : Controller
    {
        private Context db = new Context();

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Produto - Lista";
            var produtos = db.Produtos
                .Include(t => t.Usuario)
                .Where(t => t.Vendido == false)
                .OrderBy(t => t.Nome);
            return View(produtos.ToList());
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Produto - Cadastrar";
            Usuario x = (Usuario)Session["usuario"];
            ViewBag.Usuario = x.UsuarioId;
            return View();
        }

        // POST: Tarefa/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "ProdutoId,UsuarioId,Nome,Preco,Vendido")] Produto produto)
        {
            //Usuario x = (Usuario)Session["usuario"];
            //produto.UsuarioId = x.UsuarioId;

            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produto);
        }

        // GET: Produto/Comprar
        public ActionResult Comprar(int? id)
        {
            ViewBag.Title = "Produto - Comprar";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProdutoId = produto.ProdutoId;
            return View(produto);
        }

        [HttpGet]
        public ActionResult ComprarItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto p = db.Produtos.Find(id);
            Usuario u = db.Usuarios.Find(p.UsuarioId);

            u.Receita += p.Preco;
            p.Vendido = true;

            db.Entry<Produto>(p).State = EntityState.Modified;
            db.Entry<Usuario>(u).State = EntityState.Modified;
            db.SaveChanges();

            Usuario x1 = (Usuario)Session["usuario"];
            Usuario x2 = db.Usuarios.Find(x1.UsuarioId);
            Session["usuario"] = x2;

            return RedirectToAction("Index");
        }
    }
}
