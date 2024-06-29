using BookStore.Domain;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.GetAllBooks
{
    public class GetAllBooksResponse
    {
        public List<Book> Books { get; set; }

    }
}
