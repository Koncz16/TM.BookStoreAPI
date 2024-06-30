using BookStore.Application.Users.AuthenticateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.GetUserBooks
{
    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserRequest>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        }
    }
}
