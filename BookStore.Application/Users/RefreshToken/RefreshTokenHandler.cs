using BookStore.Application.Users.AuthenticateUser;
using BookStore.Data.Abstraction;
using BookStore.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly UserService userService;
        private readonly IUserRepository userRepository;


        public RefreshTokenHandler(IUserRepository userRepository, UserService userService)
        {
            this.userRepository = userRepository;

            this.userService = userService;

        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);
            if (user == null)
            {
                return new RefreshTokenResponse { IsSuccessful = false };
            }


            var newAccessToken = userService.GenerateJwtToken(user.Name);

            return new RefreshTokenResponse { IsSuccessful = true, AccessToken = newAccessToken };
        }
    }
}
