using BookStore.Data.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.InsertBook
{
    public class InsertBookHandler : IRequestHandler<InsertBookRequest, InsertBookResponse>
    {
        private readonly IBookRepository bookRepository;

        public InsertBookHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<InsertBookResponse> Handle(InsertBookRequest request, CancellationToken cancellationToken)
        {
            var id = await this.bookRepository.InsertAsync(request.Book, cancellationToken);
            var response = new InsertBookResponse
            {
                Id = id,
            };
            return response;
        }
    }
}
