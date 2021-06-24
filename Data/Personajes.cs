using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.NET.Data
{
    public class Personajes
    {
        [Key]
        public int IdPersonaje { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public string Historia { get; set; }

        public ICollection<Peliculas> Peliculas { get; set; }

    }
}
