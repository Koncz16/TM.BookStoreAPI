using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetBooksByAuthorNationality
{
    public class GetBooksByAuthorNationalityRequestValidator : AbstractValidator<GetBooksByAuthorNationalityRequest>
    {
        public GetBooksByAuthorNationalityRequestValidator()
        {
        }
    }
}
