using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using LocadoraES.ViewModel;
using LocadoraES.Models;
using LocadoraES.Utilidades;

namespace LocadoraES.Controllers
{
    public class PerfilManageController : Controller
    {
        private UsersContext db = new UsersContext();
        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(ChangePasswordViewModel chviewmodel)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;
            //informações do usuario
            var user = db.Users.FirstOrDefault(u => u.Login == login);
            if (Hash.GenerateHash(chviewmodel.SenhaAtual) != user.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }
            //se a senha estiver correta
            user.Senha = Hash.GenerateHash(chviewmodel.NovaSenha);
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Mensagem"] = "Senha alterada com sucesso";
            return RedirectToAction("Index", "Filme");
        }
    }
}