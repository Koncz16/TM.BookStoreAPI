using BookStore.Data.Abstraction;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.GetUserBooks
{
    public class GetUserBooksHandler : IRequestHandler<GetUserBooksRequest, GetUserBooksResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IBookRepository bookRepository;

        public GetUserBooksHandler(IUserRepository userRepository, IBookRepository bookRepository)
        {
            this.userRepository = userRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<GetUserBooksResponse> Handle(GetUserBooksRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var username = request.Username;
                var user = await userRepository.GetByNameAsync(username, cancellationToken);

                if (user == null)
                {
                    return new GetUserBooksResponse
                    {
                        IsSuccessful = false,
                        ErrorMessage = "User not found"
                    };
                }

                var bookIds = user.Books; //

                var books = new List<Book>();
                foreach (var bookId in bookIds)
                {
                    var book = await bookRepository.GetByIdAsync(bookId, cancellationToken);
                    if (book != null)
                    {
                        books.Add(book);
                    }
                }

                return new GetUserBooksResponse
                {
                    Books = books,
                    IsSuccessful = true
                };
            }
            catch (Exception ex)
            {
                return new GetUserBooksResponse
                {
                    IsSuccessful = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
