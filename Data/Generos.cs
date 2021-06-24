using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.NET.Data
{
    public class Generos
    {
        [Key]
        public int IdGenero { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public int IdPelicula { get; set; }

        public Peliculas Peliculas { get; set; }
    }
}
