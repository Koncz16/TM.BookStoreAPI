﻿using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetBooksByAuthorNationality
{
    public class GetBooksByAuthorNationalityRequest : IRequest<GetBooksByAuthorNationalityResponse>
    {
    }
}
