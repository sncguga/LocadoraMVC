using LocadoraES.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data;

namespace LocadoraES.Controllers
{
    public class LocacaoController : Controller
    {
     
        private LocadoraContext db = new LocadoraContext();

        // GET: Locacao
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Locacoes.ToList());
        }


        [Authorize]
        public ActionResult CadastroLocacao()
        {
            ViewBag.Filmes = db.Filmes;
            return View(new Locacao());
        }

        
        [HttpPost]
        public ActionResult CadastroLocacao(int[] FilmeIds, Locacao locacao)
        {

            if (!ModelState.IsValid)
            {
                return View("CadastroLocacao");

            }

            if(locacao.CPF == null || locacao.CPF == "")
            {
                ModelState.AddModelError("CPF", "Informar o CPF");
                return View("CadastroLocacao");
            }


            locacao.Filmes = new List<Filme>();
            foreach (int filmeId in FilmeIds)
            {
                Filme filme = db.Filmes.Find(filmeId);
                locacao.Filmes.Add(filme);
            }
            db.Locacoes.Add(locacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AlterarLocacao(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = db.Locacoes.Find(Id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.Filmes = locacao.Filmes;
            
                return View(locacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarLocacao(Locacao locacao, int[] FilmeIds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locacao);
        }


        public ActionResult ApagarLocacao(int[] Id)
        {
            if (Id == null)
            {
                ModelState.AddModelError("", "Nenhum item foi selecionado para deletar");
            }

            foreach (int item in Id)
            {
                Locacao locacao = db.Locacoes.Find(item);
                db.Locacoes.Remove(locacao);
                db.SaveChanges();
            }

            return View("Index", db.Locacoes.ToList());

        }

    }

}