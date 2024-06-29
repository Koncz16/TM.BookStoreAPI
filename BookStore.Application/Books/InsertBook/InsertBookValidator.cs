using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.InsertBook
{
    public class InsertBookValidator : AbstractValidator<InsertBookRequest>
    {
        public InsertBookValidator()
        {

            RuleFor(x => x.Book).NotNull();
            RuleFor(x => x.Book.Title).NotEmpty();
            RuleFor(x => x.Book.AuthorId).NotEmpty();
            RuleFor(x => x.Book.PublisherId).NotEmpty();

        }

    }
}
