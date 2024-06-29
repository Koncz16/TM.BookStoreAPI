using BookStore.Application.Books.DeleteBookById;
using BookStore.Application.Books.GetAllBooks;
using BookStore.Application.Books.GetBookById;
using BookStore.Application.Books.InsertBook;
using BookStore.Application.Books.UpdateBook;
using BookStore.Domain;
using BookStore.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BooksController : ControllerBase
    {
        private readonly IMediator mediator;

        public BooksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetBook/{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken token)
        {
            var response = await this.mediator.Send(new GetBookByIdRequest { Id = id }, token);

            if (response.book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }
            return this.Ok(response);

        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
        {
            var response = await this.mediator.Send(new GetAllBooksRequest(), cancellationToken);

            return this.Ok(response);
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(string id, CancellationToken cancellationToken)
        {
            var response = await this.mediator.Send(new DeleteBookByIdRequest { Id = id }, cancellationToken);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPost("InsertBook")]
        public async Task<IActionResult> InsertBook([FromBody] InsertBookRequest request, CancellationToken cancellationToken)
        {
            if (request == null || request.Book == null)
            {
                return BadRequest(new { Message = "Invalid book data" });
            }

            var response = await this.mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromBody] Book book, CancellationToken cancellationToken)
        {
            var response = await this.mediator.Send(new UpdateBookRequest { Book = book }, cancellationToken);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(new { Message = "Book not found" });
        }



        /*  Tema: 
         *  
         * GetAllAsync
         * InsertAsync
         * DeleteAsync
         * UpdateAsync
         * 
         * 
         * 
         *  AccesToken
         *  RefreshToken
         * 
         * 
         */

    }
}
