using BookStore.Data.Abstraction;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetBooksByAuthorNationality
{
    public class GetBooksByAuthorNationalityHandler : IRequestHandler<GetBooksByAuthorNationalityRequest, GetBooksByAuthorNationalityResponse>
    {
        private readonly IHomeworkRepository homeworkRepository;

        public GetBooksByAuthorNationalityHandler(IHomeworkRepository homeworkRepository)
        {
            this.homeworkRepository = homeworkRepository;
        }

        public async Task<GetBooksByAuthorNationalityResponse> Handle(GetBooksByAuthorNationalityRequest request, CancellationToken cancellationToken)
        {
           
            var books = await this.homeworkRepository.GetBooksByAuthorNationality(cancellationToken);

            return new GetBooksByAuthorNationalityResponse { Books = books };
        }
    }

}
