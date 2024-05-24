using DataAccess.Enums;
using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Models
{
    public class UserModel:Record
    {
        [DisplayName("User Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is babaaaaan!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Password { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        public Statuses Status { get; set; } 

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Role")]
        public int? RoleId { get; set; } 
     

       
        public RoleModel RoleOutput { get; set; }

        [DisplayName("Active")]
        public string IsActiveOutput { get; set; }

        
        
    }
}
