using LocadoraES.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace LocadoraES.Controllers
{
    public class GeneroController : Controller
    {
        private LocadoraContext db = new LocadoraContext();
        
     

        // GET: Genero
        [Authorize]
        public ActionResult Index()
        {
            List<Genero> generos = new List<Genero>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Locadora"].ConnectionString))
            {

                generos = db.Query<Genero>("Select * From Generos").ToList();
            }
            // return View(db.Generos.ToList());
            return View(generos);
        }



        [Authorize]
        public ActionResult CadastroGenero()
        {
            return View(new Genero());
        }

        [HttpPost]
        public ActionResult CadastroGenero(Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return View("CadastroGenero", genero);
            }

            if (genero.Nome == null || genero.Nome == "")
            {
                ModelState.AddModelError("Nome", "Informar o Gênero");
                return View("CadastroGenero");
            }

            db.Generos.Add(genero);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        public ActionResult ApagarGenero(int[] Id)
        {
            if (Id == null)
            {
                ModelState.AddModelError("", "Nenhum item foi selecionado para deletar");
            }



            foreach (int item in Id)
            {
                Genero genero = db.Generos.Find(item);
                db.Generos.Remove(genero);
                db.SaveChanges();
            }

            return View("Index", db.Generos.ToList());

        }
    }
}