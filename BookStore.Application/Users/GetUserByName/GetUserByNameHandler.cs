using BookStore.Data.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Users.GetUserByName
{
    public class GetUserByNameHandler : IRequestHandler<GetUserByNameRequest, GetUserByNameResponse>
    {
        private readonly IUserRepository userRepository;


        public GetUserByNameHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<GetUserByNameResponse> Handle(GetUserByNameRequest request, CancellationToken cancellationToken)
        {
            string name = request.Name;
            var user = await userRepository.GetByNameAsync(name, cancellationToken);
            var result = new GetUserByNameResponse
            {
                token = user.Password
            };



            return result;
        }
    }
}
