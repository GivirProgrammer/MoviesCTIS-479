using DataAccess.Enums;
using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User:Record
    {
        [Required]
        [StringLength(15)]
        public string UserName { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public Statuses Status { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public List<UserMovie> UserMovies { get; set; }
    }
}
