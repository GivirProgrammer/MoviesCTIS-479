using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class UserMovie:Record
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
