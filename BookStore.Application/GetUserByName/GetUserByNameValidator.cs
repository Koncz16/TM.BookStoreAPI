using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.GetUserByName
{
    public class GetUserByNameValidator : AbstractValidator<GetUserByNameRequest>
    {
        public GetUserByNameValidator()
        {

            this.RuleFor(x => x.Name).NotEmpty();
            this.RuleFor(x => x.Password).NotEmpty();

        }


    }
}
