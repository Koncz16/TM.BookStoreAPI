using BookStore.Application.GetBookById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.GetUserByName
{
    public class GetUserByNameRequest : IRequest<GetUserByNameResponse>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
