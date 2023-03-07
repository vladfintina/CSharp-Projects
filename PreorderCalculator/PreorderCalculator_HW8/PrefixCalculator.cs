using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderCalculator_HW8
{
    public class PrefixCalculator
    {
        private const string ADD = "+";
        private const string SUB = "-";
        private const string MUL = "*";
        private const string DIV = "/";
        private const string NEGATE = "~";

        Stack<int> myStack;

        public PrefixCalculator()
        {
            myStack = new Stack<int>();
        }

        public int CalculateExpression(string[] parameters)
        {
            for(int j = parameters.Length - 1; j >= 0; j--)
            {  
                var parameter = parameters[j];

                if (parameter == ADD || parameter == SUB || parameter == MUL || parameter == DIV)
                {
                    int operand1 = myStack.Pop();
                    int operand2 = myStack.Pop();
                    switch (parameter)
                    {
                        case ADD:
                            checked
                            {
                                int result1 = operand1 + operand2;
                                myStack.Push(result1);
                            }
                            break;
                        case SUB:
                            checked
                            {
                                int result2 = operand1 - operand2;
                                myStack.Push(result2);
                            }
                            break;
                        case MUL:
                            checked
                            {
                                int result3 = operand1 * operand2;
                                myStack.Push(result3);
                            }
                            break;
                        case DIV:
                            if (operand2 == 0)
                                throw new MyZeroDivisionExcption();
                            int result = operand1 / operand2;
                            myStack.Push(result);
                            break;
                    }
                }
                else if(parameter == NEGATE){
                    int operand = myStack.Pop();
                    operand = operand * (-1);
                    myStack.Push(operand);
                }
                else
                {
                    try
                    {
                        myStack.Push(Int32.Parse(parameter));
                    }
                    catch (Exception ex)
                    { 
                        Console.WriteLine("Format Error");
                        Environment.Exit(0);
                    }
                }

            }
            if(myStack.Count > 1)
            {
                throw new MyFormatException();
            }
            return myStack.Pop();

        }

    }
    public class MyZeroDivisionExcption : Exception
    {
        public MyZeroDivisionExcption()
        {
        }

        public MyZeroDivisionExcption(string message)
            : base(message)
        {
        }

        public MyZeroDivisionExcption(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class MyFormatException : Exception
    {
        public MyFormatException()
        {
        }

        public MyFormatException(string message)
            : base(message)
        {
        }

        public MyFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
