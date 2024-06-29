using BookStore.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.AuthenticateUser
{
    public class AuthenticateUserResponse
    {
        public bool IsSuccessful { get; set; }
        public TokenDTO TokenDTO { get; set; }
    }
}
