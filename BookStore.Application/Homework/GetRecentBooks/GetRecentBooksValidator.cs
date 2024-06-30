using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetRecentBooks
{
    public class GetRecentBooksValidator : AbstractValidator<GetRecentBooksRequest>
    {
        public GetRecentBooksValidator()
        {
        }
    }
}
