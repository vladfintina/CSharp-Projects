using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VladFintina_FinalProject.Validator
{
    internal interface Validator<T>
    {
        void validate(T value);
    }
}
