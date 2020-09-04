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
    }
}
