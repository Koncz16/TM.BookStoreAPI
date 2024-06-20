using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.InsertBook
{
    public class InsertBookValidator : AbstractValidator<InsertBookRequest>
    {
        public InsertBookValidator()
        {

            this.RuleFor(x => x.Book).NotNull();
            this.RuleFor(x => x.Book.Title).NotEmpty();
            this.RuleFor(x => x.Book.AuthorId).NotEmpty();
            this.RuleFor(x => x.Book.PublisherId).NotEmpty();

        }

    }
}
