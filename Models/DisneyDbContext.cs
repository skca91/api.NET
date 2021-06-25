using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.NET.Data
{
    public class DisneyDbContext: DbContext
    {
        //Tablas de datos
        public virtual DbSet<Personaje> Personajes { get; set; }
        public virtual DbSet<Pelicula> Peliculas { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public DisneyDbContext()
        {
        }
        public DisneyDbContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pelicula>()
                .HasOne(b => b.Generos)
                .WithOne(i => i.Peliculas)
                .HasForeignKey<Genero>(b => b.IdPelicula);
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
