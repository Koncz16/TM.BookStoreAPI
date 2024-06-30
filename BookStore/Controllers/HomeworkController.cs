using BookStore.Application.Homework.GetBooksByAuthorNationality;
using BookStore.Application.Homework.GetBooksByGenres;
using BookStore.Application.Homework.GetRecentBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class HomeworkController : ControllerBase
    {
        private readonly IMediator mediator;

        public HomeworkController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("authors-by-nationality")]
        public async Task<IActionResult> GetBooksByAuthorNationality(CancellationToken cancellationToken)
        {


            var response = await mediator.Send(new GetBooksByAuthorNationalityRequest(), cancellationToken);
            return Ok(response.Books);
        }

        [HttpGet("recent-books")]
        public async Task<IActionResult> GetRecentBooks(CancellationToken cancellationToken)
        {
            var request = new GetRecentBooksRequest(); 
            var response = await mediator.Send(request, cancellationToken); 

            return Ok(response); 
        }


        [HttpGet("by-genres")]
        public async Task<IActionResult> GetBooksByGenres([FromQuery] List<string> genres, CancellationToken cancellationToken)
        {
            var request = new GetBooksByGenresRequest();
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response.Books);
        }

    }
}
