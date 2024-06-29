using BookStore.Application.Books.InsertBook;
using BookStore.Application.Helper;
using BookStore.Data.Abstraction;
using BookStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IUserRepository userRepository;

        public RegisterUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            // Checking if the username already exists
            var existingUser = await userRepository.GetByNameAsync(request.Username, cancellationToken);
            if (existingUser != null)
            {
                return new RegisterUserResponse { IsSuccessful = false, ErrorMessage = "Username already exists" };
            }

            var hashedPassword = HashHelper.HashPassword(request.Password);

            var newUser = new User
            {
                Name = request.Username,
                Password = hashedPassword
            };

             var id = await userRepository.InsertAsync(newUser, cancellationToken);

            var response = new RegisterUserResponse
            {
                Id = id,
                IsSuccessful = true,
            };
            return response;
        }
    }
}
