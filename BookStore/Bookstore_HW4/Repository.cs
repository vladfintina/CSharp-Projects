using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Bookstore_HW4
{
	//
	// Model (Repository)
	//

	public class ModelStore
	{
		protected List<Book> books = new List<Book>();
		protected List<Customer> customers = new List<Customer>();

		public IList<Book> GetBooks()
		{
			return books;
		}

		public Book GetBook(int id)
		{
			return books.Find(b => b.Id == id);
		}

		public Customer GetCustomer(int id)
		{
			return customers.Find(c => c.Id == id);
		}

		public static ModelStore LoadFrom(TextReader reader)
		{
			var store = new ModelStore();

			try
			{
				
				if (reader.ReadLine() != "DATA-BEGIN")
				{
					return null;
				}
				while (true)
				{
					string line = reader.ReadLine();
					//Console.WriteLine(line);
					if (line == null)
					{
						return null;
					}
					else if (line == "DATA-END")
					{
						break;
					}

					string[] tokens = line.Split(';');
					switch (tokens[0])
					{
						case "BOOK":
							store.books.Add(new Book
							{
								Id = int.Parse(tokens[1]),
								Title = tokens[2],
								Author = tokens[3],
								Price = decimal.Parse(tokens[4])
							});
							break;
						case "CUSTOMER":
							store.customers.Add(new Customer
							{
								Id = int.Parse(tokens[1]),
								FirstName = tokens[2],
								LastName = tokens[3]
							});
							break;
						case "CART-ITEM":
							var customer = store.GetCustomer(int.Parse(tokens[1]));
							if (customer == null)
							{
								return null;
							}
							customer.ShoppingCart.Items.Add(new ShoppingCartItem
							{
								BookId = int.Parse(tokens[2]),
								Count = int.Parse(tokens[3])
							});
							break;
						default:
							return null;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is FormatException || ex is IndexOutOfRangeException)
				{
					return null;
				}
				throw;
			}

			return store;
		}
	}

	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public decimal Price { get; set; }
	}

	public class Customer
	{
		private ShoppingCart shoppingCart;

		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public ShoppingCart ShoppingCart
		{
			get
			{
				if (shoppingCart == null)
				{
					shoppingCart = new ShoppingCart();
				}
				return shoppingCart;
			}
			set
			{
				shoppingCart = value;
			}
		}

		public int shoppinngCartSize()
        {
			return this.ShoppingCart.Items.Count;
        }
	}

	public class ShoppingCartItem
	{
		public int BookId { get; set; }
		public int Count { get; set; }
	}

	public class ShoppingCart
	{
		public int CustomerId { get; set; }
		public List<ShoppingCartItem> Items = new List<ShoppingCartItem>();

		public void Add(Book myBook)
        {
			var book = Items.Find(b => b.BookId == myBook.Id);
			if (book == null)
            {
				ShoppingCartItem bookItem = new ShoppingCartItem();
				bookItem.BookId = myBook.Id;
				bookItem.Count = 1;
				Items.Add(bookItem);
            }
            else
            {
				book.Count++;
            }
        }

		public void Remove(Book myBook)
        {
			var book = Items.Find(b => b.BookId == myBook.Id);
			if (book == null)
            {
				throw new InvalidRequest("Invalid Request!");
            }
            else
            {
				book.Count--;
				if(book.Count == 0)
                {
					Items.Remove(book);
                }
            }
		}
	}
}

