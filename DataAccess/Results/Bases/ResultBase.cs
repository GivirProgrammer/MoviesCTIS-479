using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Results.Bases
{
    public abstract class ResultBase
    {
        protected ResultBase(bool isSuccessfull, string? message)
        {
            this.isSuccessfull = isSuccessfull;
            Message = message;
        }

        public bool isSuccessfull { get; }
        public string ?Message { get; }
    }
}
