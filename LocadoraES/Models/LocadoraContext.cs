using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocadoraES.Models
{
    public class LocadoraContext : DbContext
    {

        public LocadoraContext() : base("Locadora")
        {

        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
    }
}