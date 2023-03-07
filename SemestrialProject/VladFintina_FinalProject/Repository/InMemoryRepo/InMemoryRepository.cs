using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VladFintina_FinalProject.Domain;
using VladFintina_FinalProject.Exceptions;

namespace VladFintina_FinalProject.Repository.InMemoryRepo
{

    public class InMemoryRepository<E> : Repository<E> where E : Movie
    {
        protected List<E> movieList;

        public InMemoryRepository()
            {
                movieList = new List<E>();
            }

        /***
         * method which return if a given title already exists in our movieList or not
         * @param string title- title of the movie we are concerned of
         * @return true - if one of the movies has the same title
         *         false - if none of the movies has the same title
         * ***/
        protected bool existingElement(string title)
        {
            foreach (var movie in movieList)
            {
                if (title == movie.getTitle())
                    return true;
            }
            return false;

        }

        /*** 
         * @param movie - the movie which will be added
         *                  it must not be null
         * ***/
        public virtual void addElement(E movie)
        {
            if(existingElement(movie.getTitle()))
            {
                throw new ExistanceException("This movie is already in the list: " + movie.getTitle());
            }
            movieList.Add(movie);
        }

        /***
         * updates the existing movie with a new one(title must remain the same)
         * @param movie - must not be null
         * @throws ExistanceException if the movie does not exist in repository
         ***/
        public virtual void modifyElement(E movie)
        {
            bool found = false;
            for(int i = 0; i< movieList.Count; i++)
            {

                if (movieList[i].getTitle() == movie.getTitle())
                {
                    found = true;
                    movieList[i] = movie;
                    break;
                }
            }
            if(found == false)
            {
                throw new ExistanceException("There is no movie with the title: " + movie.getTitle());
            }
        }

        /***
         * Method which returns the whole list of movies that are stored by the repository
         * @param no parameters
         * @return movie list of type Movie
         * ***/
        public virtual List<E> getElements()
        {
            return movieList;
        }
        /***
        * Receives the title of a movie and looks for it to delete it from the List
        * @param string title
        * @throws ExistingException if there is no movie with the given title
        ***/
        public virtual void removeMovie(string title)
        {
            bool found = false;
            for (int i = 0; i < movieList.Count; i++)
            {
                if (movieList[i].getTitle() == title)
                {
                    movieList.RemoveAt(i);
                    found = true;
                    break;
                }
            }
            if(!found)
                throw new ExistanceException("There is not any movie with the given title!");

        }

        /***
         * Method which search a movie by a given title and returns it if found, else ExistanceException is thrown
         * @param string title
         * @throws ExistingException if there is no movie with the given title
         * ***/
        public virtual E searchMovie(string title)
        {
            foreach(E movie in movieList)
            {
                if (movie.getTitle() == title)
                    return movie;
            }

            throw new ExistanceException("There is not any movie with the given title!");
        }
    }
}
