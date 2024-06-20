using BookStore.Data.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.UpdateBook
{
    public  class UpdateBookHandler : IRequestHandler<UpdateBookRequest, UpdateBookResponse>
    {
        private readonly IBookRepository bookRepository;

        public UpdateBookHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<UpdateBookResponse> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var result = await this.bookRepository.UdpateAsync(request.Book, cancellationToken);
            return new UpdateBookResponse { Success = result };
        }
    }
}
