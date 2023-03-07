using System;
using System.Collections.Generic;
using System.IO;

namespace Bookstore_HW4
{
    internal class Program
    {

        private static void Main(string[] args)
        {
         
                TextReader reader = Console.In;
                ModelStore myModelStore = ModelStore.LoadFrom(reader);
                if(myModelStore == null)
                {
                    Console.WriteLine("Data error.");
                    Environment.Exit(0);
                }
                //StreamReader reader = new StreamReader(args[0]);
                //ModelStore myModelStore = ModelStore.LoadFrom(reader);
                Printer myPrinter = new Printer();
                Service myService = new Service(myModelStore, myPrinter);
                //StreamReader reader = new StreamReader(Console.OpenStandardInput());
                //StreamReader reader = new StreamReader(args[0]);
                using (reader)
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        myService.ProcessRequest(line);
                    }
                }
        }
    }
}