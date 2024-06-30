using BookStore.Domain.DTOs;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Abstraction
{
    public interface IHomeworkRepository
    {
        public  Task<List<Book>> GetBooksByAuthorNationality(CancellationToken cancellationToken);
        public Task<List<Book>> GetRecentBooks(CancellationToken cancellationToken);

        public  Task<List<BookOutDto>> GetBooksByGenresAndAddAuthorName(CancellationToken cancellationToken);

    }
}
