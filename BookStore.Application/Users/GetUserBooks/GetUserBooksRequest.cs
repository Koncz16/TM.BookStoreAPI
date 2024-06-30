using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.GetUserBooks
{
    public class GetUserBooksRequest : IRequest<GetUserBooksResponse>
    {
        public string Username { get; set; }
    }
}
