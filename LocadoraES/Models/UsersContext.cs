using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocadoraES.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base("Locadora")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}