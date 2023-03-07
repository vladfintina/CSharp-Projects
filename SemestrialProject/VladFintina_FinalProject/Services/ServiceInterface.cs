using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VladFintina_FinalProject.Domain;

namespace VladFintina_FinalProject.Services
{
    public interface ServiceInterface
    {
       
        public void addMovie(string title, string genre, string mainActor, int year);
        
        public void removeMovie(string title);

        public void updateMovie(Movie movie);

        public Movie searchMovie(string title);

        public List<Movie> getMovies();

        public List<Movie> filterMovieByGenre(string genre);

        public List<Movie> sortMoviesByYear();

    }
}
