using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.NET.Models
{
    public class Movie
    {

        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime Creation { get; set; }
        public int Score { get; set; }
        public Genre Genre{ get; set; }
        public ICollection<Character> Characters { get; set; }


    }
}
