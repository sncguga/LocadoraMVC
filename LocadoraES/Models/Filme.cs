using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LocadoraES.Models
{
    [Table("Filmes")]
    public class Filme
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Display(Name = "Data de criação")]
        public DateTime DhCriacao { get; set; }

        [Display(Name = "Ativo")]
        public bool FlAtivo { get; set; }


        [Required(ErrorMessage = "Escolha/Cadastre um gênero")]
        [ForeignKey("Genero")]
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "Escolha/Cadastre um gênero")]
        public int IdLocacao { get; set; }

        public virtual ICollection<Locacao> Locacoes { get; set; }

        public virtual Genero Genero { get; set; }

        
        public virtual List<Genero> ListaGenero()
        { return new List<Genero>(); }

       

    }


}
