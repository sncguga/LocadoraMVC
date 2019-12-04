using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocadoraES.Models;
using Dapper;

namespace LocadoraES.Controllers
{
    public class FilmeController : Controller
    {
        private LocadoraContext db = new LocadoraContext();

        
        // GET: Filme
        [Authorize]
        public ActionResult Index()
        {
           return View(db.Filmes.ToList());
           
        }

        [Authorize]
        public ActionResult CadastroFilme()
        {
            //carrega o id do genero para o dropdown
            ViewBag.IdGenero = new SelectList(db.Generos.ToList(),
           "Id", "Nome");
            return View(new Filme());
        }

        
        [HttpPost]
        public ActionResult CadastroFilme(Filme filme)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.IdGenero = new SelectList(db.Generos.ToList(),
           "Id", "Nome");
                
                return View("CadastroFilme", filme);

            }

            if (filme.Nome == null || filme.Nome == "")
            {
                ModelState.AddModelError("Nome", "Informar o Nome");
                return View("CadastroFilme");
            }

            if (filme.IdGenero == 0)
            {
                ModelState.AddModelError("IdGenero", "Informar o Gênero");
                return View("CadastroFilme");
            }


            //retorna o Id do genero
            ViewBag.IdGenero = new SelectList(
            new Filme().ListaGenero(), "Id", "Nome", filme.IdGenero);
            db.Filmes.Add(filme);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AlterarFilme(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Filme filme = db.Filmes.Find(Id);
            if (filme == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGenero = new SelectList(db.Generos.ToList(),
          "Id", "Nome");
            return View(filme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarFilme([Bind(Include = "Id, Nome, DhCriacao, FlAtivo, IdGenero")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                ViewBag.IdGenero = new SelectList(
               new Filme().ListaGenero(), "Id", "Nome", filme.IdGenero);
                db.Entry(filme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filme);
        }


        public ActionResult ApagarFilme(int[] Id)
        {
            if (Id == null)
            {
                ModelState.AddModelError("", "Nenhum item foi selecionado para deletar");
            }

            foreach (int item in Id)
            {
                Filme filme = db.Filmes.Find(item);
                db.Filmes.Remove(filme);
                db.SaveChanges();
            }

            return View("Index", db.Filmes.ToList());

        }

    }
}