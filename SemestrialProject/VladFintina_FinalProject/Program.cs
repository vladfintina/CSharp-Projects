using System;
using System.Collections.Generic;
using VladFintina_FinalProject.Services;
using VladFintina_FinalProject.Domain;
using VladFintina_FinalProject.Repository.InMemoryRepo;
using VladFintina_FinalProject.UI;
using VladFintina_FinalProject.Validator;
using VladFintina_FinalProject.Repository.FileRepo;

namespace VladFintina_FinalProject
{
    public class Program
    {
        private static void Main(string[] args)
        {
            InMemoryRepository<Movie> repository = new InMemoryRepository<Movie>();
            FileRepository<Movie> fileRepo = new FileRepository<Movie>("data.txt");
            MovieValidator movieValidator = new MovieValidator();

            Service service = new Service(fileRepo, movieValidator);

            ConsoleUI consoleUI = new ConsoleUI(service);

            consoleUI.showUI();

            
        }
    }
}