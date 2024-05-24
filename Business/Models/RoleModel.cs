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
    public class RoleModel:Record
    {
        #region Entity Properties
        [DisplayName("Role Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Name { get; set; }
        #endregion

        #region Extra Properties
        [DisplayName("User Count")]
        public int UserCount { get; set; }

        public string Users { get; set; }
        #endregion
    }
}
