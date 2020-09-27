using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Conrete
{
    public class EFBookRepository :IBookRepository
    {
        EEDbContext context = new EEDbContext();
        public IEnumerable<Book> Books
        {
            get { return context.Books; }
        }

        public void SaveBook(Book book)
        {
            if(book.Id == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.Find(book.Id);
                if (dbEntry != null){
                    dbEntry.Name = book.Name;
                    dbEntry.Author = book.Author;
                    dbEntry.Description = book.Description;
                    dbEntry.Genre = book.Genre;
                    dbEntry.Price = book.Price;
                }
            }
            context.SaveChanges();

        }
        public void DeleteBook(Book book)
        {
            if (book.Id > 0)
            {
                /*context.Books.Remove(book);*/
                context.Database.ExecuteSqlCommand("DELETE FROM Books WHERE Id = " + book.Id);
            }
            context.SaveChanges();
        }
    }
}
