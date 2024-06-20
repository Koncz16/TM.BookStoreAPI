using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Abstraction
{
    public interface IBookRepository :IRepository<Book>
    {

    }
}
