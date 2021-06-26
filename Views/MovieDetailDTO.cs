using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.NET.Views
{
    public class MovieDetailDTO
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime Creation { get; set; }
        public int Score { get; set; }
        public ICollection<CharacterDTO> characters { get; set; }
    }
}
