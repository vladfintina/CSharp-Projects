using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VladFintina_FinalProject.Services;
using VladFintina_FinalProject.Domain;
using VladFintina_FinalProject.Exceptions;


namespace VladFintina_FinalProject.UI
{
    public class ConsoleUI : UI
    {
        private ServiceInterface myService;

        public ConsoleUI(ServiceInterface service)
        {
            myService = service;
        }

        public void showUI()
        {
            bool running = true;

            while(running)
            {
                showMenu();
                Console.WriteLine("Introduce the number of the command: ");
                string cmdLine = Console.ReadLine();
                int command = -1;
                try
                {
                    command = int.Parse(cmdLine);
                }
                catch(Exception ex) { continue; }

                try
                {
                    switch (command)
                    {
                        case 0:
                            {
                                running = false;
                                break;
                            }
                        case 1:
                            {
                                string title;
                                string genre;
                                string mainActor;
                                int year;
                                Console.WriteLine("Introduce title: ");
                                title = Console.ReadLine();

                                Console.WriteLine("Introduce genre: ");
                                genre = Console.ReadLine();

                                Console.WriteLine("Introduce Main Actor: ");
                                mainActor = Console.ReadLine();

                                Console.WriteLine("Introduce year: ");
                                string sYear = Console.ReadLine();

                                year = int.Parse(sYear);

                                myService.addMovie(title, genre, mainActor, year);

                                break;

                            }
                        case 2:
                            {
                                List<Movie> movies = myService.getMovies();
                                int i = 1;
                                foreach (Movie movie in movies)
                                {
                                    Console.WriteLine(i + "." + movie);
                                    i++;
                                }
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Introduce title of movie to be deleted:");
                                string title = Console.ReadLine();
                                myService.removeMovie(title);
                                break;
                            }
                        case 4:
                            {
                                string title;
                                string genre;
                                string mainActor;
                                int year;
                                Console.WriteLine("Introduce title of the movie you would like to update: ");
                                title = Console.ReadLine();

                                Console.WriteLine("Introduce new genre: ");
                                genre = Console.ReadLine();

                                Console.WriteLine("Introduce new Main Actor: ");
                                mainActor = Console.ReadLine();

                                Console.WriteLine("Introduce new year: ");
                                string sYear = Console.ReadLine();

                                year = int.Parse(sYear);

                                Movie movie = new Movie(title, genre, mainActor, year);
                                myService.updateMovie(movie);

                                break;
                            }
                        case 5:
                            {
                                Console.WriteLine("Introduce title of the movie you would like to search: ");
                                string title = Console.ReadLine();
                                Movie movie = myService.searchMovie(title);
                                Console.WriteLine("Your movie is:" + movie);
                                break;
                            }
                        case 6:
                            {
                                Console.WriteLine("Introduce the genre you want: ");
                                string genre = Console.ReadLine();
                                List<Movie> filteredList = myService.filterMovieByGenre(genre);
                                if(filteredList.Count() == 0)
                                {
                                    Console.WriteLine("There is no movie with the given genre!");
                                }
                                else
                                {
                                    Console.WriteLine("The movies are:");
                                    foreach(Movie movie in filteredList)
                                    {
                                        Console.WriteLine(movie);
                                    }
                                }
                                break;
                            }
                        case 7:
                            {
                                List<Movie> sortedList = myService.sortMoviesByYear();
                                if (sortedList.Count() == 0)
                                {
                                    Console.WriteLine("The list of movies is empty!");
                                }
                                else
                                {
                                    Console.WriteLine("The movies are:");
                                    foreach (Movie movie in sortedList)
                                    {
                                        Console.WriteLine(movie);
                                    }
                                }
                                break;

                            }
                         default:
                            {
                                Console.WriteLine("Invalid command");
                                break;
                            }
                    }
                }
                catch(ExistanceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(ValidationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(Exception ex) when (ex is ArgumentNullException || ex is OverflowException || ex is FormatException)
                {
                    Console.WriteLine("Number is required and something else was introduced!");
                }
                
            }
        }

        void showMenu()
        {
            Console.WriteLine("MENU:");
            Console.WriteLine("1. Add new movie");
            Console.WriteLine("2. Show all movies");
            Console.WriteLine("3. Delete movie");
            Console.WriteLine("4. Update movie");
            Console.WriteLine("5. Search movie by title");
            Console.WriteLine("6. Filter movies by genre");
            Console.WriteLine("7. Sort movies by debut year");
            Console.WriteLine("0. EXIT");
        }

    }
}
