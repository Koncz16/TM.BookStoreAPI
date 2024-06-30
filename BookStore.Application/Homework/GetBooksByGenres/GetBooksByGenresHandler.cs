using BookStore.Application.Homework.GetBooksByAuthorNationality;
using BookStore.Data.Abstraction;
using BookStore.Domain.DTOs;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetBooksByGenres
{
    public class GetBooksByGenresHandler : IRequestHandler<GetBooksByGenresRequest, GetBooksByGenresResponse>
    {
        private readonly IHomeworkRepository homeworkRepository;

        public GetBooksByGenresHandler(IHomeworkRepository homeworkRepository)
        {
            this.homeworkRepository = homeworkRepository;
        }

        public async Task<GetBooksByGenresResponse> Handle(GetBooksByGenresRequest request, CancellationToken cancellationToken)
        {

            var books = await this.homeworkRepository.GetBooksByGenresAndAddAuthorName(cancellationToken);

            return new GetBooksByGenresResponse { Books = books };
        }
    }
}
