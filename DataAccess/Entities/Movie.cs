using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Movie:Record
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public List<UserMovie> UserMovies { get; set; }
    }
}
