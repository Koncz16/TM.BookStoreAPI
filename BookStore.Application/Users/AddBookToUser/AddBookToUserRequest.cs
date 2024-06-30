using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.AddBookToUser
{
    public class AddBookToUserRequest : IRequest<AddBookToUserResponse>
    {
        public string Username { get; set; }
        public string BookId { get; set; }
    }
}
