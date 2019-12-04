using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraES.Models
{
    [Table("Generos")]
    public class Genero
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome")]
        [MaxLength(100)]

        public string Nome { get; set; }

        [Display(Name = "Data de Criação")]
        public DateTime DhCriacao { get; set; }

        [Display(Name = "Ativo")]
        public bool FlAtivo { get; set; }
    }
}
