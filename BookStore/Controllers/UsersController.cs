using Amazon.Runtime.Credentials.Internal;
using BookStore.Application.GetBookById;
using BookStore.Application.GetUserByName;
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

        private readonly SymmetricSecurityKey key;
        private readonly TokenValidationParameters tokenValidationParameters;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("09d25e094faa6ca2556c818166b7a9563b93f7099f6f0f4caa6cf63b88e8d3e7"));

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
        public IActionResult Authenticate(string username, string password)
        {
            //var user = userService.Authenticate(username, password);

            //if (user == null)
            //    return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new { token = response });

        }
    }
}
