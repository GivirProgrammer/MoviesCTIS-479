using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Models
{
    public class FavoriteModel
    {
        public int MovieId { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Movie Name")]
        public string MovieName { get; set; }
    }
}
