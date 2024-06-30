using BookStore.Domain;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Abstraction
{
    public interface IBookRepository :IRepository<Book>
    {
        public  Task<List<Book>> GetBooksByIdsAsync(List<string> bookIds, CancellationToken cancellationToken);


    }
}
