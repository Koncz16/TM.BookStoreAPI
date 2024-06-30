using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.AddBookToUser
{
    public class AddBookToUserValidator : AbstractValidator<AddBookToUserRequest>
    {
        public AddBookToUserValidator()
        {
            RuleFor(x => x.BookId).NotEmpty().WithMessage("Book Id must not be empty");
        }
    }
}
