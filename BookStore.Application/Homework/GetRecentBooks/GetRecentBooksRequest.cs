using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Homework.GetRecentBooks
{
    public class GetRecentBooksRequest : IRequest<GetRecentBooksResponse>
    {
    }
}
