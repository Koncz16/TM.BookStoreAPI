using Amazon.Runtime.Credentials.Internal;
using BookStore.Application.Users.AddBookToUser;
using BookStore.Application.Users.AuthenticateUser;
using BookStore.Application.Users.GetUserByName;
using BookStore.Application.Users.RefreshToken;
using BookStore.Application.Users.RegisterUser;
using BookStore.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.Application.Users.GetUserBooks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsersController :ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserService userService;

        private readonly SymmetricSecurityKey key;
        private readonly TokenValidationParameters tokenValidationParameters;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
            this.userService = userService;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var response = await mediator.Send(request);
            if (!response.IsSuccessful)
            {
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    return BadRequest(new { Message = response.ErrorMessage });
                }
                else
                {
                    return BadRequest(new { Message = "Registration failed" });
                }
            }

            return Ok(new { Message = "Registration successful" });
        }


        //[HttpGet("GetUser/{name}/{password}")]
        //public async Task<IActionResult> GetUserByName(string name, string password, CancellationToken cancellationToken)
        //{
        //    var request = new GetUserByNameRequest { Name = name,Password= password };
        //    var response = await this.mediator.Send(request, cancellationToken);

        //    if (response == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(response);
        //}


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(request, cancellationToken);
            if (!response.IsSuccessful)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            return Ok(response.TokenDTO);
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(refreshTokenRequest, cancellationToken);
            if (!response.IsSuccessful)
            {
                return Unauthorized(new { Message = "Invalid refresh token" });
            }

            return Ok(new { AccessToken = response.AccessToken });
        }


        [HttpPost("addbook")]
        public async Task<IActionResult> AddBookToUser([FromBody] AddBookToUserRequest request)
        {
            try
            {

                var response = await mediator.Send(request);

                if (!response.IsSuccessful)
                {
                    if (!string.IsNullOrEmpty(response.ErrorMessage))
                    {
                        return BadRequest(new { Message = response.ErrorMessage });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Failed to add book to user" });
                    }
                }

                return Ok(new { Message = "Book added to user successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

        [HttpGet("books/{username}")]
        public async Task<IActionResult> GetUserBooks(string username)
        {
            var request = new GetUserBooksRequest { Username = username };
            var response = await mediator.Send(request);

            if (!response.IsSuccessful)
            {
                return BadRequest(new { Message = response.ErrorMessage });
            }

            return Ok(new { Books = response.Books });
        }

    }

}