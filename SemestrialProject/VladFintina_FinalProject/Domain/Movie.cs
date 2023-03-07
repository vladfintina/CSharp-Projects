using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VladFintina_FinalProject.Domain
{
    public class Movie
    {
        private string title;
        private string genre;
        private string mainActor;
        private int year;


        public Movie(string title, string genre, string mainActor, int year)
        {
            this.title = title;
            this.genre = genre;
            this.mainActor = mainActor;
            this.year = year;
        }

        public Movie(Movie movie)
        {
            this.title = movie.getTitle();
            this.genre = movie.getGenre();
            this.mainActor = movie.getMainActor();
            this.year = movie.getYear();
        }

        /***
         * Getters for th attributes of the movie
         * title - string
         * genre - string
         * main actor - string
         * year - int
         * ***/
        public string getTitle()
        {
            return this.title;
        }

        public string getGenre()
        {
            return this.genre;
        }

        public string getMainActor()
        {
            return this.mainActor;
        }

        public int getYear()
        {
            return this.year;
        }

        /***
         * Setter for the attributes of the movie:
         * title - string
         * genre - string
         * main actor - string
         * year - int
         * ***/
        public void setTitle(string title)
        {
            this.title = title;
        }

        public void setGenre(string genre)
        {
            this.genre = genre;
        }

        public void setMainActor(string mainActor)
        {
            this.mainActor = mainActor;
        }

        public void setYear(int year)
        {
            this.year = year;
        }

        /***
         * Method used to find out if two movies are equivalent.
         * Two movies are equivalent if they have the same title.
         * ***/
        bool compare(Movie movie)
        {
            if (movie == null)
                return false;
            if (this.title == movie.getTitle())
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            string movieString;
            movieString = this.title + "," + this.genre + "," + this.mainActor + "," + this.year;
            return movieString;
        }
    }
}
