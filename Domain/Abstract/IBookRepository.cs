using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IBookRepository
    {
         IEnumerable<Book> Books { get; }
    }
}
