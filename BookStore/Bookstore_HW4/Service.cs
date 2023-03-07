using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_HW4
{
    public class Service
    {
        private ModelStore modelStore;
        private Printer printer;

        public Service(ModelStore modelStore, Printer printer)
        {
            this.modelStore = modelStore;
            this.printer = printer;
        }

        

        public Service() { }//used for testing

        private bool CanStringBeNumber(string input)
        {
            string numbers = "0123456789";
            for (int i = 0; i < input.Length; i++)
                if (!numbers.Contains(input[i]))
                    return false;
            return true;
        }

        //method that validates the format of a request
        //throws InvalidRequest if format is not valid
        public void ValidateRequestFormat(string request)
        {
            string[] words = request.Split(' ');
            if (words.Length != 3)
                throw new InvalidRequest("Invalid Request");
            if (!words[0].Equals("GET"))
                throw new InvalidRequest("Invalid Request");
            string numbers = "0123456789";
            for (int i = 0; i < words[1].Length; i++)
                if (!numbers.Contains(words[1][i]))
                    throw new InvalidRequest("Invalid Request");

            string[] commands = words[2].Split("/");
            //foreach (string word in commands)
              //  Console.WriteLine(word);
            if (commands.Length != 4 && commands.Length != 6)
                throw new InvalidRequest("Invalid Request");
            if (commands.Length == 4)
            {
                if (commands[0] != "http:")
                    throw new InvalidRequest("Invalid Request");
                if(commands[1] != "")
                    throw new InvalidRequest("Invalid Request");
                if (commands[2] != "www.nezarka.net")
                    throw new InvalidRequest("Invalid Request");
                if (commands[3] != "Books" && commands[3]!= "ShoppingCart")
                    throw new InvalidRequest("Invalid Request");

            }
            if (commands.Length == 6)
            {
                if (commands[0] != "http:")
                    throw new InvalidRequest("Invalid Request");
                if (commands[1] != "")
                    throw new InvalidRequest("Invalid Request");
                if (commands[2] != "www.nezarka.net")
                    throw new InvalidRequest("Invalid Request");
                if (commands[3] != "Books" && commands[3] != "ShoppingCart")
                    throw new InvalidRequest("Invalid Request");
                if (commands[3] == "Books")
                    if (commands[4] != "Detail" || !CanStringBeNumber(commands[5]))
                        throw new InvalidRequest("Invalid Request");

                if (commands[3] == "ShoppingCart")
                    if ( (commands[4] != "Add" && commands[4] != "Remove") || !CanStringBeNumber(commands[5]))
                        throw new InvalidRequest("Invalid Request");

            }
        }

        IList<Book> GetAllBooks()
        {
            return modelStore.GetBooks();

        }

        Customer searchCustomerID(int id)
        {
            return modelStore.GetCustomer(id);
        }

        Book searchBookID(int id)
        {
            return modelStore.GetBook(id);
        }


        public void ProcessRequest(string request)
        {
            int customerId = -1;

            try
            {
                ValidateRequestFormat(request);
            }
            catch (InvalidRequest ex)
            {
                printer.PrintInvalidRequest();
                return;

            }

            string[] commands = request.Split(' ');
            try
            {
                customerId = Int32.Parse(commands[1]);
            }
            catch (Exception ex) 
            {
                printer.PrintInvalidRequest();
                return;
            }

            Customer myCustomer = null;
            if((myCustomer = searchCustomerID(customerId)) == null)
            {
                printer.PrintInvalidRequest();
                return;
            }
            
            string[] parameters = commands[2].Split('/');

            if(parameters.Length == 4)
            {

                if (parameters[3] == "Books")
                    PrepareBooksToPrint(myCustomer);

                if (parameters[3] == "ShoppingCart")
                    PrepareShoppingCartToPrint(myCustomer);
            }    

            if(parameters.Length == 6)
            {
                int bookId = -1;
                try
                {
                    bookId = Int32.Parse(parameters[5]);
                }
                catch (Exception ex)
                {
                    printer.PrintInvalidRequest();
                    return;
                }

                if (parameters[4] == "Detail")
                {

                    Book myBook = null;
                    if ((myBook = searchBookID(bookId)) == null)
                    {
                        printer.PrintInvalidRequest();
                        return;
                    }
                    PrepareBookDetailToPrint(myCustomer, myBook);

                }

                if(parameters[4] == "Add")
                {
                    Book myBook = null;
                    if ((myBook = searchBookID(bookId)) == null)
                    {
                        printer.PrintInvalidRequest();
                        return;
                    }
                    PrepareAddOperation(myCustomer, myBook);
                }

                if (parameters[4] == "Remove")
                {
                    Book myBook = null;
                    if ((myBook = searchBookID(bookId)) == null)
                    {
                        printer.PrintInvalidRequest();
                        return;
                    }
                    PrepareRemoveOperation(myCustomer, myBook);
                }
            }
        }

        void PrepareBooksToPrint(Customer customer)
        {
            string customerFirstName = customer.FirstName;
            var books = GetAllBooks();
            int cartSize = customer.shoppinngCartSize();
            string shoppingCartSize = cartSize.ToString();

            printer.PrintAllBooks(customerFirstName, shoppingCartSize, books);


        }

        void PrepareShoppingCartToPrint(Customer customer)
        {
            var customerShopppingCart = customer.ShoppingCart.Items;
            List<Tuple<Book, decimal, int>> customerBooks = new List<Tuple<Book, decimal, int>>();     
            int cartSize = customer.shoppinngCartSize();
            decimal cartTotal = 0;
            for (int i = 0; i < cartSize; i++)
            {
                var book = this.modelStore.GetBook(customerShopppingCart[i].BookId);
                int count = customerShopppingCart[i].Count; 
                customerBooks.Add(new Tuple<Book, decimal, int>(book,book.Price, count));
                cartTotal += book.Price * count;
            }
            printer.PrintShoppingCart(customerBooks, cartSize, customer.FirstName, cartTotal);
        }


        void PrepareBookDetailToPrint(Customer customer, Book book)
        {
            int cartSize = customer.shoppinngCartSize();
            string customerFirstName = customer.FirstName;
            printer.PrintBookDetail(customerFirstName, book, cartSize);

        }

        void PrepareAddOperation(Customer myCustomer, Book myBook)
        {
            myCustomer.ShoppingCart.Add(myBook);
            PrepareShoppingCartToPrint(myCustomer);

        }

        void PrepareRemoveOperation(Customer myCustomer, Book myBook)
        {
            try
            {
                myCustomer.ShoppingCart.Remove(myBook);
            }
            catch (InvalidRequest ex)
            {
                printer.PrintInvalidRequest();
                return;
            }
            PrepareShoppingCartToPrint(myCustomer);
        }
    }

    



    public class InvalidRequest : Exception
    {
        public InvalidRequest() { }

        public InvalidRequest(string message) : base(message) { }

        public InvalidRequest(string message, Exception inner):base(message, inner) { }

    }

}
