using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VladFintina_FinalProject.Exceptions
{
    public class ValidationException: Exception
    {
        /***
         * Exception thrown when there is an invalid element
         * @param message of the invalid components 
         * ***/
        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception innerEx) : base(message, innerEx) { }
    }
}
