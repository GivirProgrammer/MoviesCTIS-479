using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Producer:Record
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
