using BookStore.Application.Helper;
using BookStore.Data.Abstraction;
using BookStore.Domain.DTOs;
using BookStore.Domain.Models;
using BookStore.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.AuthenticateUser
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserRequest, AuthenticateUserResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly UserService userService;

        public AuthenticateUserHandler(IUserRepository userRepository, UserService userService)
        {
            this.userRepository = userRepository;
            this.userService = userService;
        }

        public async Task<AuthenticateUserResponse> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByNameAsync(request.Username, cancellationToken);
            if (user == null)
            {
                return new AuthenticateUserResponse { IsSuccessful = false };
            }

          

            // Your authentication logic here (password validation, etc.)
            bool isValidPassword = HashHelper.VerifyPassword(user.Password, request.Password);

            if (!isValidPassword)
            {
                return new AuthenticateUserResponse { IsSuccessful = false };
            }

            // Generate new refresh token and set expiry time (7 days from now)
            var newRefreshToken = userService.GenerateRefreshToken();
            var newExpiryTime = DateTime.UtcNow.AddDays(7);

            // Update user entity with new refresh token and expiry time
            await userRepository.UpdateRefreshTokenAsync(user.Id, newRefreshToken, newExpiryTime, cancellationToken);

            // Generate JWT token
            var token = userService.GenerateJwtToken(request.Username);

            return new AuthenticateUserResponse
            {
                IsSuccessful = true,
                TokenDTO = new TokenDTO(token, newRefreshToken)
            };
        }
    }

}
