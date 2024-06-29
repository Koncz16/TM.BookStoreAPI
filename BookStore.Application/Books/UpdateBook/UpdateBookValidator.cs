using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.UpdateBook
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookValidator()
        {

            RuleFor(x => x.Book).NotNull();
            RuleFor(x => x.Book.Title).NotEmpty();
            RuleFor(x => x.Book.AuthorId).NotEmpty();
            RuleFor(x => x.Book.PublisherId).NotEmpty();

        }

    }
}
