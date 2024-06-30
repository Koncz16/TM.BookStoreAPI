using BookStore.Domain.DTOs;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetRecentBooks
{
    public class GetRecentBooksResponse
    {
        public List<Book> RecentBooks { get; set; } = new List<Book>();
    }
}
