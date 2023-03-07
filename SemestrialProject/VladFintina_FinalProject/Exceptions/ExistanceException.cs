using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VladFintina_FinalProject.Exceptions
{
    public class ExistanceException: Exception
    {
        /***
       * Exception thrown when some data already exists in the repository (or doesn't exist at all)
       * @param message the error message
        */
        public ExistanceException(string message) : base(message) { }
    }
}
