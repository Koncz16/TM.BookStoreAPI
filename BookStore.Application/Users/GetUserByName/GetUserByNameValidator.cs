using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.GetUserByName
{
    public class GetUserByNameValidator : AbstractValidator<GetUserByNameRequest>
    {
        public GetUserByNameValidator()
        {

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();

        }


    }
}
