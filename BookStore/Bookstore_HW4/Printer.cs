using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_HW4
{
    public class Printer
    {

        //careful, this method open <html...> but it doesnt close it
        void PrintHeader()
        {
            Console.WriteLine("<!DOCTYPE html>");
            Console.WriteLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            Console.WriteLine("<head>");
            Console.WriteLine("\t<meta charset=\"utf-8\" />");
            Console.WriteLine("\t<title>Nezarka.net: Online Shopping for Books</title>");
            Console.WriteLine("</head>");
        }


        //be careful, this method does not take care of the right above <body>
        public void PrintStyleParagraph()
        {
            Console.WriteLine("\t<style type=\"text/css\">");
            Console.WriteLine("\t\ttable, th, td {");
            Console.WriteLine("\t\t\tborder: 1px solid black;");
            Console.WriteLine("\t\t\tborder-collapse: collapse;");
            Console.WriteLine("\t\t}");
            Console.WriteLine("\t\ttable {");
            Console.WriteLine("\t\t\tmargin-bottom: 10px;");
            Console.WriteLine("\t\t}");
            Console.WriteLine("\t\tpre {");
            Console.WriteLine("\t\t\tline-height: 70%;");
            Console.WriteLine("\t\t}");
            Console.WriteLine("\t</style>");

        }

        public void PrintPersonalMenu(string customerFirstName, string cartSize)
        {
            Console.WriteLine("\t<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>");
            Console.WriteLine("\t" + customerFirstName + ", here is your menu:");
            Console.WriteLine("\t<table>");
            Console.WriteLine("\t\t<tr>");
            Console.WriteLine("\t\t\t<td><a href=\"/Books\">Books</a></td>");
            Console.WriteLine("\t\t\t<td><a href=\"/ShoppingCart\">Cart (" + cartSize + ")</a></td>");
            Console.WriteLine("\t\t</tr>");
            Console.WriteLine("\t</table>");


        }
        public void PrintInvalidRequest()
        {
            PrintHeader();
            Console.WriteLine("<body>");
            Console.WriteLine("<p>Invalid request.</p>");
            Console.WriteLine("</body>");
            Console.WriteLine("</html>");
            Console.WriteLine("====");
        }

        public void PrintAllBooks(string customerFirstName, string cartSize, IList<Book>books)
        {
            PrintHeader();
            Console.WriteLine("<body>");
            PrintStyleParagraph();
            PrintPersonalMenu(customerFirstName, cartSize);
            Console.WriteLine("\tOur books for you:");
            Console.WriteLine("\t<table>");

            if(books.Count == 0)
            {  
                Console.WriteLine("\t</table>");
            }
            else
            {
                for(int i=0; i<books.Count; i++)
                {
                    var book = books[i];
                    if (i % 3 == 0)
                        Console.WriteLine("\t\t<tr>");
                    Console.WriteLine("\t\t\t<td style=\"padding: 10px;\">");
                    Console.WriteLine("\t\t\t\t<a href=\"/Books/Detail/"+ book.Id.ToString() +"\">" + book.Title + "</a><br />");
                    Console.WriteLine("\t\t\t\tAuthor: " + book.Author + "<br />");
                    Console.WriteLine("\t\t\t\tPrice: " + book.Price.ToString() + " EUR &lt;<a href=\"/ShoppingCart/Add/" + book.Id.ToString() + "\">Buy</a>&gt;");
                    Console.WriteLine("\t\t\t</td>");
                    if (i % 3 == 2)
                        Console.WriteLine("\t\t</tr>");
                    if (i == books.Count - 1 && i % 3 != 2)
                        Console.WriteLine("\t\t</tr>");
                }
                Console.WriteLine("\t</table>");
            }

            Console.WriteLine("</body>");
            Console.WriteLine("</html>");
            Console.WriteLine("====");

        }

        public void PrintShoppingCart(List<Tuple<Book,decimal,int>> customerShoppingCart, int cartSize, string customerFirstName, decimal cartTotal)
        {
            PrintHeader();
            Console.WriteLine("<body>");
            PrintStyleParagraph();
            PrintPersonalMenu(customerFirstName, cartSize.ToString());
            if(cartSize == 0)
            {
                Console.WriteLine("\tYour shopping cart is EMPTY.");
            }
            else
            {
                Console.WriteLine("\tYour shopping cart:");
                Console.WriteLine("\t<table>");
                Console.WriteLine("\t\t<tr>");
                Console.WriteLine("\t\t\t<th>Title</th>");
                Console.WriteLine("\t\t\t<th>Count</th>");
                Console.WriteLine("\t\t\t<th>Price</th>");
                Console.WriteLine("\t\t\t<th>Actions</th>");
                Console.WriteLine("\t\t</tr>");
                foreach( var tuple in customerShoppingCart )
                {
                    var myBook = tuple.Item1;
                    var myPrice = tuple.Item2;
                    var bookCount = tuple.Item3;
                    Console.WriteLine("\t\t<tr>");
                    Console.WriteLine("\t\t\t<td><a href=\"/Books/Detail/" + myBook.Id.ToString() + "\">" + myBook.Title + "</a></td>");
                    Console.WriteLine("\t\t\t<td>" + bookCount.ToString() + "</td>");
                    if(bookCount == 1 )
                    {
                        Console.WriteLine("\t\t\t<td>" + myPrice.ToString() + " EUR</td>");
                    }
                    else
                    {
                        decimal totalPrice = myPrice * bookCount;
                        string stringPrice = bookCount.ToString() + " * " + myPrice.ToString() + " = " + totalPrice.ToString();
                        Console.WriteLine("\t\t\t<td>" + stringPrice + " EUR</td>");

                    }
                    Console.WriteLine("\t\t\t<td>&lt;<a href=\"/ShoppingCart/Remove/" + myBook.Id + "\">Remove</a>&gt;</td>");
                    Console.WriteLine("\t\t</tr>");
                }

                Console.WriteLine("\t</table>");
                Console.WriteLine("\tTotal price of all items: " + cartTotal.ToString() + " EUR");
            }

            Console.WriteLine("</body>");
            Console.WriteLine("</html>");
            Console.WriteLine("====");

        }

        public void PrintBookDetail(string customerFirstName, Book myBook, int cartSize)
        {
            PrintHeader();
            Console.WriteLine("<body>");
            PrintStyleParagraph();
            PrintPersonalMenu(customerFirstName, cartSize.ToString());
            Console.WriteLine("\tBook details:");
            Console.WriteLine("\t<h2>"+ myBook.Title +"</h2>");
            Console.WriteLine("\t<p style=\"margin-left: 20px\">");
            Console.WriteLine("\tAuthor: " + myBook.Author + "<br />");
            Console.WriteLine("\tPrice: " + myBook.Price.ToString() +" EUR<br />");
            Console.WriteLine("\t</p>");
            Console.WriteLine("\t<h3>&lt;<a href=\"/ShoppingCart/Add/" + myBook.Id.ToString() + "\">Buy this book</a>&gt;</h3>");
            Console.WriteLine("</body>");
            Console.WriteLine("</html>");
            Console.WriteLine("====");
        }


    }
}
