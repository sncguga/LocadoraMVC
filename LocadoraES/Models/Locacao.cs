using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraES.Models
{
    [Table("Locacoes")]
    public class Locacao
    {
        public int Id { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Preencha o CPF")]
        public string CPF { get; set; }

        [Display(Name = "Data de Criação")]

        public DateTime DhCriacao { get; set; }
        public virtual List<Filme> Filmes { get; set; }
    }
}
