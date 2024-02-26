using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Publisher:Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
