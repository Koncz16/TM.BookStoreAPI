using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetBooksByGenres
{
    public class GetBooksByGenresRequestValidator : AbstractValidator<GetBooksByGenresRequest>
    {
        public GetBooksByGenresRequestValidator()
        {
        }
    }
}
