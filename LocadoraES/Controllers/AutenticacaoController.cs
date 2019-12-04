using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocadoraES.Models;
using LocadoraES.ViewModel;
using LocadoraES.Utilidades;
using System.Security.Claims;

namespace LocadoraES.Controllers
{
    public class AutenticacaoController : Controller
    {
        private UsersContext db = new UsersContext();

        // GET: Autenticacao
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastroUsersViewModel cviewmodel)
        {   //verifica se os dados são valido, caso contrario retorna para view
            if (!ModelState.IsValid)
            {
                return View(cviewmodel);
            }
            //verificar se ja existe usuario cadastrado com mesmo login
            if (db.Users.Count(u => u.Login == cviewmodel.Login) > 0)
            {
                ModelState.AddModelError("Login", "Já existe usuário com este Login");
                return View(cviewmodel);
            }
            User newUser = new User
            {
                Nome = cviewmodel.Nome,
                Login = cviewmodel.Login,
                Senha = Hash.GenerateHash(cviewmodel.Senha)
              
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            //mensagem de cadastro
            TempData["Mensagem"] = "Cadastro realizado com sucesso. Preceda o login.";
            return RedirectToAction("Login");
        }

        public ActionResult Login(string ReturnUrl)
        {
            var lviewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };

            return View(lviewmodel);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel lviewmodel)
        {
            //verificar erro de validação
            if (!ModelState.IsValid)
            {
                return View(lviewmodel);
            }

            //localizar usuario no DB
            var user = db.Users.FirstOrDefault(u => u.Login == lviewmodel.Login);
            //verificando dados login
            if (user == null)
            {
                ModelState.AddModelError("Login", "Login incorreto");
                return View(lviewmodel);
            }
             if (user.Senha != Hash.GenerateHash(lviewmodel.Senha)) // ajustar qndo arrumar criptografia
            
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(lviewmodel);
            }

            //define identidade do usuario
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim("Login", user.Login)
            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            //redirecionamento do usuario logado
            if (!String.IsNullOrWhiteSpace(lviewmodel.UrlRetorno) || Url.IsLocalUrl(lviewmodel.UrlRetorno))
                return Redirect(lviewmodel.UrlRetorno);
            else
                return RedirectToAction("Index", "Filme");
        }
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}