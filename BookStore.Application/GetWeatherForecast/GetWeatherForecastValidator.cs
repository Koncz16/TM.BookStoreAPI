﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.GetWeatherForecast
{
    public class GetWeatherForecastValidator : AbstractValidator<GetWeatherForecastRequest>
    {
        public GetWeatherForecastValidator() {
            //this.RuleFor(request => request.Id).NotEmpty().NotNull();

        }
    }
}
