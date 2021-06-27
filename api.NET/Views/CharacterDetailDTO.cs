using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.NET.Views
{
    public class CharacterDetailDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public string Story { get; set; }
        public ICollection<MovieDTO> movies { get; set; } 
    }
}
