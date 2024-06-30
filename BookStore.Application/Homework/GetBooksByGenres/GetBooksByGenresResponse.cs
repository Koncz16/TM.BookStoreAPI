using BookStore.Domain.DTOs;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetBooksByGenres
{
    public class GetBooksByGenresResponse
    {
        public List<BookOutDto> Books { get; set; }

    }
}
