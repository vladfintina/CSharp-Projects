using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Homework_2
{

    public class InputController
    {
        string inFile;
        string outFile;
        int maxWidth;
        public StreamReader reader;
        public StreamWriter writer;
        Queue<string> words = new Queue<string>();
        int lineLength = 0;

        /*In the constructor are verified the integrity of the input and the stream reader/writer are set up */
        public InputController(string[] arguments)
        {
            if (arguments.Length != 3)
            {
                System.Console.WriteLine("Argument Error");
                Environment.Exit(0);
            }
            inFile = arguments[0];
            outFile = arguments[1];
            try {
                maxWidth = Int32.Parse(arguments[2]);
                if(maxWidth < 0)
                {
                    Console.WriteLine("Argument Error");
                    Environment.Exit(0);
                }
            }
            catch(Exception ex)
            {
                if (ex is FormatException || ex is OverflowException || ex is ArgumentNullException)
                {
                    Console.WriteLine("Argument Error");
                    Environment.Exit(0);
                }
            }
            try
            {
                reader = new StreamReader(inFile);
                writer = new StreamWriter(outFile);
            }
            catch (IOException ex)
            {
                Console.WriteLine("File Error");
                Environment.Exit(0);
            }
            
        }
            
        /*
            function the splits the words from the given text and send them to another function WordsArrangement and also an int
            0 -for usual word
            1 -for the end of file
            2 -for \n

         */
         public void WordSplitting() { 
            try
            {
                string word = "";
                string previousWord = "";
                char[] separators = { '\n', ' ', '\t', '\r' };
                int emptyLines = 0;

                while (!reader.EndOfStream)
                {
                    char character = (char)reader.Read();
                    string ch = character.ToString();

                    if( ch != "\r")
                    {
                        if (!separators.Contains(character))
                        {
                            if (emptyLines > 0 && ch != "\n")
                            {
                                emptyLines = 0;
                                string endLine = "\n";
                                WordsArrangement(endLine, 2);
                            }
                            word += ch;
                            previousWord = ch;

                        }
                        else
                        {
                            if (previousWord == "\n" && ch == "\n")
                            {
                                emptyLines++;
                            }
                            if (word.Length > 0)
                            {
                                WordsArrangement(word, 0);
                                word = "";
                            }
                           
                            if(ch !=  " " && ch!="\t")
                                previousWord = ch;
                        }
                    }
                }
                if(word.Length > 0 && word!="\n")
                    WordsArrangement(word, 1);
                else
                    WordsArrangement("", 1);

            }
            catch (IOException ex)
            {
                //Console.WriteLine(ex.ToString());
                Console.WriteLine("File Error");
                Environment.Exit(0);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error");
                Environment.Exit(0);
            }

         }


        void Case0(int lineLen)//when the maxWidth is reached, Case0 prints yhem properly on the line
        {
            int queueLength = words.Count;
            lineLen--;
            int nrSpaces = 0;
            int restOfSpaces = 0;
            string lineToPrint="";
            if(queueLength == 1)
            {
                lineToPrint += words.Dequeue();
                for (int i = 0; i < lineToPrint.Length; i++)
                {
                    writer.Write(lineToPrint[i]);
                }
                writer.Write("\n");
            }
            else 
            {
                nrSpaces = 1 + (maxWidth - lineLen) / (queueLength - 1);//number of spaces that should be added between each 2 words
                restOfSpaces = (maxWidth - lineLen) % (queueLength - 1);

                for (int i = 0; i < queueLength; i++)
                {
                    lineToPrint += words.Dequeue();
                    if (i != queueLength - 1)
                        for (int j = 0; j < nrSpaces; j++)
                            lineToPrint += " ";
                    if (restOfSpaces > 0)
                    {
                        lineToPrint += " ";
                        restOfSpaces--;
                    }
                }


                //Console.WriteLine(lineToPrint);
                for (int i = 0; i < lineToPrint.Length; i++)
                {
                    writer.Write(lineToPrint[i]);
                }
                if(lineToPrint.Length > 0)
                    writer.Write("\n");
            }
            
        }

        void Case2(int lineLen) // we must prin an empty line, before we search if we have any words left in the queue
        {
            if (words.Count > 0)
                Case1(lineLen);
            //Console.WriteLine();
            writer.Write("\n");

        }

        void Case1(int lineLen)
        {
            int queueLength = words.Count;
            string lineToPrint = "";

            for(int i = 0 ; i < queueLength ; i++)
            {
                lineToPrint += words.Dequeue() ;
                if (i != queueLength - 1)
                    lineToPrint += " ";
            }
            //Console.WriteLine(lineToPrint);
            for(int i = 0; i < lineToPrint.Length ; i++)
            {
                writer.Write(lineToPrint[i]);
            }
            writer.Write("\n");
        }

        void WordsArrangement(string word, int myCase)
        {

            //Console.WriteLine(word + myCase);
            
            switch(myCase)
            {

                case 2://case of /n
                    Case2(lineLength);
                    lineLength = 0;

                    break;
                case 0://case of usual word
                    if (word.Length + lineLength + 1 <= maxWidth + 1)
                    {
                        words.Enqueue(word);
                        lineLength += word.Length + 1;
                    }
                    else
                    {
                        if (words.Count == 0)
                        {
                            words.Enqueue(word);
                            Case0(lineLength);
                        }
                        else
                        {
                            Case0(lineLength);
                            lineLength = 0;
                            words.Enqueue(word);
                            lineLength = word.Length + lineLength + 1;
                        }                   
                        
                    }
                    break;
                case 1://case of endOfFile
                    if(word == "")
                    {
                        Case1(lineLength);
                    }
                    else
                    {
                        if (word.Length + lineLength + 1 <= maxWidth + 1)
                        {
                            words.Enqueue(word);
                            lineLength += word.Length + 1;
                            Case1(lineLength);
                        }
                        else
                        {
                            Case0(lineLength);
                            lineLength = 0;
                            words.Enqueue(word);
                            lineLength = lineLength + word.Length + 1;
                            Case1(lineLength);

                        }
                    }
                   
                    break;
            }

            /*if(myCase == 2)
            {
                Case2(lineLength);
                lineLength = 0;
            }

            if(word.Length + lineLength + 1 <= maxWidth + 1 && )
            {
                words.Enqueue(word);
                lineLength += word.Length + 1 ;
            }
            else
            {
                Case0(lineLength);
                lineLength = 0;
                words.Enqueue(word);
                lineLength = word.Length + lineLength + 1 ;
            }*/

           // if(word.Length + lineLength + 1 <= maxWidth + 1 && myCase == 1)
            //lastWord = word;

        }

    }

        internal class Program
        {
            private static void Main(string[] args)
            {
                var MyInputController = new InputController(args);
                MyInputController.WordSplitting();
                MyInputController.writer.Close();
                MyInputController.reader.Close();
                

            }
        }
    
}