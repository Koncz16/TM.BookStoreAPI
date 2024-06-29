using BookStore.Application.GetWeatherForecast;
using BookStore.Data.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.GetBookById
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdRequest, GetBookByIdResponse>
    {

        private readonly IBookRepository bookRepository;
        public GetBookByIdHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<GetBookByIdResponse> Handle(GetBookByIdRequest request, CancellationToken cancellationToken)
        {

            string id = request.Id;
            var book = await bookRepository.GetByIdAsync(id, cancellationToken);
            var result = new GetBookByIdResponse { book = book };


            return result;
        }

        //public async Task<GetBookByIdResponse> Handler (GetBookByIdRequest request, CancellationToken cancellationToken)
        //{
        //    string id = request.Id;
        //    var book = await this.bookRepository.GetByIdAsync(id, cancellationToken);
        //    return new GetBookByIdResponse {book = book };
        //}

    }
}
