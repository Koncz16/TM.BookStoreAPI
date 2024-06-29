using BookStore.Domain.DTOs;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStore.Domain.Mappings
{
    public static class BookMapper
        {
            public static BookDTO MapToDTO(Book book)
            {
                return new BookDTO
                {
                    Title = book.Title,
                    AuthorId = book.AuthorId,
                    PublisherId = book.PublisherId,
                    YearOfPublication = book.YearOfPublication,
                    Genres = book.Genres
                };
            }

            public static Book MapToModel(BookDTO bookDTO)
            {
                return new Book
                {
                    Title = bookDTO.Title,
                    AuthorId = bookDTO.AuthorId,
                    PublisherId = bookDTO.PublisherId,
                    YearOfPublication = bookDTO.YearOfPublication,
                    Genres = bookDTO.Genres
                };
            }
        }
}