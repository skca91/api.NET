using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.NET.Models
{
    public class DisneyDbContext: DbContext
    {
        //Tablas de datos
        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public DisneyDbContext()
        {
        }
        public DisneyDbContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasOne(b => b.Genre)
                .WithOne(i => i.Movie)
                .HasForeignKey<Genre>(b => b.IdMovie);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //En caso de que el contexto no este configurado, lo configuramos mediante la cadena de conexion
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=127.0.0.1;user=steph;password=1234;database=disney", new MySqlServerVersion(new Version(5, 7, 31)));
            }
        }

        
    }
}
