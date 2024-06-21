using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Abstraction
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByNameAsync(string username, CancellationToken cancellationToken);


    }
}
