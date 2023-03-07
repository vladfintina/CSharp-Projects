using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VladFintina_FinalProject.Domain;
using VladFintina_FinalProject.Exceptions;

namespace VladFintina_FinalProject.Validator
{
    public class MovieValidator : BaseValidator<Movie>
    {
        string exception;
        public override void validate(Movie myMovie)
        {
            this.exception = "";

            if(myMovie.getTitle() == "")
            {
                exception += "Invalid title!\n";
            }
            if(myMovie.getGenre() == "")
            {
                exception += "Invalid genre\n";
            }
            if(myMovie.getMainActor() == "")
            {
                exception += "Invalid Main Actor!\n";
            }
            if(myMovie.getYear() < 1900 || myMovie.getYear() > 2023)
            {
                exception += "Invalid year!\n";
            }

            if(exception != "")
                throw new ValidationException(exception);

        }
    }
}
