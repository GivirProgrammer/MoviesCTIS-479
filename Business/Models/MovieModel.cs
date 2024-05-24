using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Models
{
    public class MovieModel:Record
    {
        [Required(ErrorMessage = "{0} is required!")]
        // Way 1:
        //[StringLength(100, MinimumLength = 2, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        // Way 2:
        [MinLength(2, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Movie Name")]
        public string Name { get; set; }

        [DisplayName("Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [DisplayName("Producer")]
        public int PublisherId { get; set; }

        [DisplayName("Users")]
        public List<int> UsersInput { get; set; }

        public List<UserModel> Users { get; set; }

        [DisplayName("Publisher")]
        public string PublisherOutput { get; set; }

        [DisplayName("Release Date")]
        public string ReleaseDateOutput { get; set; }


    }
}
