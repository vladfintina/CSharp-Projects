using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderCalculator_HW8
{
   
    internal class Program
    {
        public const string ZeroDivisionError = "Divide Error";
        public const string FormatError = "Format Error";
        public const string OverflowError = "Overflow Error";

        private static void Main(string[] args)
        {
            string inputLine = Console.ReadLine();
            string[] parameters = inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            PrefixCalculator myCalculator = new PrefixCalculator();
            int expressionResult = 0;
            try
            {
                expressionResult = myCalculator.CalculateExpression(parameters);
                Console.WriteLine(expressionResult);

            }
            catch (MyZeroDivisionExcption ex)
            {
                Console.WriteLine(ZeroDivisionError);
            }
            catch(OverflowException ex)
            {
                Console.WriteLine(OverflowError);
            }
            catch(Exception ex)
            {
                Console.WriteLine(FormatError);
            }
        }
    }
}