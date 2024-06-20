using BookStore.Application.GetBookById;
using BookStore.Data.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DeleteBookById
{
    public class DeleteBookByIdHandler : IRequestHandler<DeleteBookByIdRequest, DeleteBookByIdResponse>
    {


            private readonly IBookRepository bookRepository;
            public DeleteBookByIdHandler(IBookRepository bookRepository)
            {
                this.bookRepository = bookRepository;
            }


        public async Task<DeleteBookByIdResponse> Handle(DeleteBookByIdRequest request, CancellationToken cancellationToken)
        {
            string id = request.Id;
            var ok = await this.bookRepository.DeleteAsync(id, cancellationToken);
            var result = new DeleteBookByIdResponse { Success = ok};


            return result;
        }
    }
    }
