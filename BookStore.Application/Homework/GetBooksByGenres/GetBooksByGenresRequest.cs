using BookStore.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetBooksByGenres
{
    public class GetBooksByGenresRequest : IRequest<GetBooksByGenresResponse>
    {
    }
}
