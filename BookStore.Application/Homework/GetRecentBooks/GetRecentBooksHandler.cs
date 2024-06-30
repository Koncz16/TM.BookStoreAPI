using BookStore.Application.Homework.GetBooksByAuthorNationality;
using BookStore.Data.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetRecentBooks
{
    public class GetRecentBooksHandler : IRequestHandler<GetRecentBooksRequest, GetRecentBooksResponse>
    {
        private readonly IHomeworkRepository homeworkRepository;

        public GetRecentBooksHandler(IHomeworkRepository homeworkRepository)
        {
            this.homeworkRepository = homeworkRepository;
        }

        public async Task<GetRecentBooksResponse> Handle(GetRecentBooksRequest request, CancellationToken cancellationToken)
        {
            var books = await this.homeworkRepository.GetRecentBooks(cancellationToken);

            return new GetRecentBooksResponse { RecentBooks = books };
        }
    }
}
