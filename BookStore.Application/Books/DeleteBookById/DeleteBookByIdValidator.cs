using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.DeleteBookById
{
    public class DeleteBookByIdValidator : AbstractValidator<DeleteBookByIdRequest>
    {
        public DeleteBookByIdValidator()
        {
            RuleFor(request => request.Id).NotEmpty();
        }
    }
}
