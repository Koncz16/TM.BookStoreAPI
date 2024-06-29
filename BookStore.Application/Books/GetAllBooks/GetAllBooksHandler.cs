using BookStore.Data.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.GetAllBooks
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksRequest, GetAllBooksResponse>
    {
        private readonly IBookRepository bookRepository;
        public GetAllBooksHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public async Task<GetAllBooksResponse> Handle(GetAllBooksRequest request, CancellationToken cancellationToken)
        {
            var books = await bookRepository.GetAllAsync(cancellationToken);
            var result = new GetAllBooksResponse
            {
                Books = books
            };

            return result;
        }
    }
}
