using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.GetBookById
{
    public class GetBookByIdRequest : IRequest<GetBookByIdResponse>
    {
        public string Id { get; set; }
    }
}
