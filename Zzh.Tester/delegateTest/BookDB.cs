using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzh.Tester
{
    public struct Book
    {
        public string Title { get; set; }       // Title of the book.
        public string Author;       // Author of the book.
        public decimal Price;       // Price of the book.
        public bool Paperback;      // Is it paperback?

        public Book(string title, string author, decimal price, bool paperBack)
        {
            Title = title;
            Author = author;
            Price = price;
            Paperback = paperBack;
        }
    }
   public class BookDB
    {
        // List of all books in the database:
        ArrayList list = new ArrayList();

        // Add a book to the database:
        public void AddBook(string title, string author, decimal price, bool paperBack)
        {
            list.Add(new Book(title, author, price, paperBack));
        }

        // Call a passed-in delegate on each paperback book to process it: 
        public void ProcessPaperBooks(ProcessBookDelegate processBook)
        {
            foreach (Book book in list)
            {
                if (book.Paperback)
                    processBook(book);
            }
        }
    }
    public delegate void ProcessBookDelegate(Book book);
    class PriceTotaller
    {
        int countBooks = 0;
        decimal priceBooks = 0.0m;

        public void AddBookToTotal(Book book)
        {
            countBooks += 1;
            priceBooks += book.Price;
        }

        public decimal AveragePrice()
        {
            return priceBooks / countBooks;
        }
    }
}
