using BookStore.Domain;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Books.UpdateBook
{
    public class UpdateBookRequest : IRequest<UpdateBookResponse>
    {
        public Book Book { get; set; }

    }
}
