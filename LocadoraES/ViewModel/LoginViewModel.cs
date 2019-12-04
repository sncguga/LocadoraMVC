using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocadoraES.ViewModel
{
    public class LoginViewModel
    {
        //UrlReturn para garantir que o usuario volte a pagina que tentava acessar apos o login
        [HiddenInput]
        public string UrlRetorno { get; set; }

        [Required(ErrorMessage = "Preencha o campo de login")]
        [MaxLength(50, ErrorMessage = "O login deve ter até 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a sua senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; }
    }
}