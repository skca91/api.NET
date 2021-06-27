using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.NET.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Movie = new HashSet<Movie>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public ICollection<Movie> Movie { get; set; }
    }
}
