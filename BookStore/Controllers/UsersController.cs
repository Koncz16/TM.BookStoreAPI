using Amazon.Runtime.Credentials.Internal;
using BookStore.Application.Users.AuthenticateUser;
using BookStore.Application.Users.GetUserByName;
using BookStore.Application.Users.RefreshToken;
using BookStore.Application.Users.RegisterUser;
using BookStore.Services;
using MediatR;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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


        [HttpGet("GetUser/{name}/{password}")]
        public async Task<IActionResult> GetUserByName(string name, string password, CancellationToken cancellationToken)
        {
            var request = new GetUserByNameRequest { Name = name,Password= password };
            var response = await this.mediator.Send(request, cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }


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
    }

}