using BookStore.Domain;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.GetBookById
{
    public class GetBookByIdResponse
    {
        public Book? book { get; set; }
    }
}
