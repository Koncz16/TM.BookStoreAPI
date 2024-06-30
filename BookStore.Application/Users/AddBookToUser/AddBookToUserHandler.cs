using BookStore.Data.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.AddBookToUser
{
    public class AddBookToUserHandler : IRequestHandler<AddBookToUserRequest, AddBookToUserResponse>
    {
        private readonly IUserRepository userRepository;

        public AddBookToUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<AddBookToUserResponse> Handle(AddBookToUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetByNameAsync(request.Username, cancellationToken);
                if (user == null)
                {
                    return new AddBookToUserResponse { IsSuccessful = false, ErrorMessage = "User not found" };
                }

                user.Books.Add(request.BookId);

                // Mentés az adatbázisba
                await userRepository.AddBookToUserAsync(user, cancellationToken);

                return new AddBookToUserResponse { IsSuccessful = true };
            }
            catch (Exception ex)
            {
                return new AddBookToUserResponse { IsSuccessful = false, ErrorMessage = ex.Message };
            }
        }
    }
}
