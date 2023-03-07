using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VladFintina_FinalProject.Repository;
using VladFintina_FinalProject.Domain;
using VladFintina_FinalProject.Validator;
using VladFintina_FinalProject.Repository.InMemoryRepo;

namespace VladFintina_FinalProject.Services
{
    public class Service: ServiceInterface
    {
        private InMemoryRepository<Movie> repository;
        private MovieValidator movieValidator;

        /// <summary>
        /// Creates an entity of our class and requires a valid repository and a validator that will be used
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="movieValid"></param>
        public Service(InMemoryRepository<Movie> repository, MovieValidator movieValid)
        {
            this.repository = repository;
            movieValidator = movieValid;
        }

        /***
         * Receives the attributes of a movie, creates the instance of it, it validates it and then add it to the repository
         * @param string title, string genre, string Main Actor, int year
         * @throws ValidationException if the movie is invalid
         * @throws ExistingException if the movie already exists
         * ***/
        public void addMovie(string title, string genre, string mainActor, int year)
        {
            Movie movie = new Movie(title, genre, mainActor, year);
            movieValidator.validate(movie);
            repository.addElement(movie);
        }

        /***
       * Receives the title of a movie and calls repository to delete it
       * @param string title
       * @throws ExistingException if there is no movie with the given title
       * ***/
        public void removeMovie(string title)
        {
            repository.removeMovie(title);
        }

        /***
       * Receives the an entity of a movie
       * @param Movie movie
       * @throws ValidationException if the movice is invalid
       * @throws ExistingException if the movie does not exist
       * ***/
        public void updateMovie(Movie movie)
        {
            repository.modifyElement(movie);

        }

        /***
         * Return the list of all available movies from the repository
         * @return whole list of available movies
         * @param no parameters
         * ***/
        public List<Movie> getMovies()
        {
            return repository.getElements();
        }

        /***
       * Receives the title of a movie and calls repository to search for it
       * @param string title
       * @return an entity of Movie, if there exists one with the given title
       * @throws ExistingException if there is no movie with the given title
       * ***/
        public Movie searchMovie(string title)
        {
            return repository.searchMovie(title);
        }

        /***
         * Filters the list of movies according to the given genre. It returns a list of movies hving the given genre
         * @param string genre
         * @return List<Movie> filteredList
         * ***/
        public List<Movie> filterMovieByGenre(string genre)
        {
            List<Movie> filteredList = new List<Movie> ();
            var fullList = getMovies();
            foreach (Movie movie in fullList)
            {
                if(movie.getGenre() == genre)
                    filteredList.Add(movie);
            }

            return filteredList;
        }

        /***
         * Sorts the list of movies by the debut year of the movie
         * @param no parameters
         * @return sortedList type Movie
         * ***/
        public List<Movie> sortMoviesByYear()
        {
            List<Movie> movieList = getMovies();
            List<Movie> sortedList = movieList.OrderBy(m => m.getYear()).ToList();
            return sortedList;
        }


    }
}
