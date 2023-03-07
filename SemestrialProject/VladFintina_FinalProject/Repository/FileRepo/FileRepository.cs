using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VladFintina_FinalProject.Repository.InMemoryRepo;
using VladFintina_FinalProject.Domain;
using System.IO;

namespace VladFintina_FinalProject.Repository.FileRepo
{
    public class FileRepository<E> : InMemoryRepository<E> where E : Movie
    {
        private readonly string fileName;

        public FileRepository(string fileName)
        {
            this.fileName = fileName;
            loadData();
        }

        /***
         * Loads all data from the source file and adds it in the repository
         * ***/
        private void loadData()
        {
            using var reader = new StreamReader(fileName);
            string line = reader.ReadLine();
            while(line != null)
            {
                List<string> data = line.Split(",").ToList();
                string title = data[0];
                string genre = data[1];
                string mainActor = data[2];
                int year = int.Parse(data[3]);
                Movie movie = new Movie(title, genre, mainActor, year);
                base.addElement((E)movie);
                line = reader.ReadLine();
            }
        }

        /***
         * Append a given movie to the source file
         * @param Movie movie
         * ***/
        private void writeMovieToFile(Movie movie)
        {
            using var writer = new StreamWriter(fileName, true);
            writer.WriteLine(movie.ToString());
        }

        /***
         * Deletes everything from the source file, leaving it empty
         * ***/
        private void clearFile()
        {
            using var writer = new StreamWriter(fileName);
            writer.Write("");
        }


        /***
         * Writes all the movies from the repository in the source file
         * ***/
        private void writeAllMoviesToFile()
        {
            List<E> movies = base.getElements();
            using var writer = new StreamWriter(fileName, true);
            foreach (E movie in movies)
            {
                writer.WriteLine(movie.ToString());
            }

        }

        /***
         * Calls the base class to add a movie and if there is no exception thrown, the new movie is written in the source file
         * ***/
        public override void addElement(E movie)
        {
            base.addElement(movie);
            writeMovieToFile(movie);
        }

        /***
         * Calls the base class to modify a movie and if there is no exception thrown it clears the file and then all movies are written
         * in the source file
         * ***/
        public override void modifyElement(E movie)
        {
            base.modifyElement(movie);
            clearFile();
            writeAllMoviesToFile();
        }

        /***
        * Calls the base class to delete a movie and if there is no exception thrown it clears the file and then all movies are written
        * in the source file
        * ***/
        public override void removeMovie(string title)
        {
            base.removeMovie(title);
            clearFile();
            writeAllMoviesToFile();
        }

        /***
         * Calls the base class to return the list of movies
         * ***/
        public override List<E> getElements()
        {
            return base.getElements();
        }

        /***
         * Calls the base class to return the searched movie
         * ***/
        public override E searchMovie(string title)
        {
            return base.searchMovie(title);
        }
    }
}
