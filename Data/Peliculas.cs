﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.NET.Data
{
    public class Peliculas
    {

        [Key]
        public int IdPelicula { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime Creacion { get; set; }
        public int Calificacion { get; set; }
        public Generos Generos { get; set; }
        public ICollection<Personajes> Personajes { get; set; }


    }
}