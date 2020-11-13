using Microsoft.EntityFrameworkCore;
using NavitaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Patrimonio> Patrimonios { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}