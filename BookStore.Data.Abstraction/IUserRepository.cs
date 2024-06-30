using BookStore.Domain;
using BookStore.Domain.Models;
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
        public Task UpdateRefreshTokenAsync(string userId, string refreshToken, DateTime refreshTokenExpiryTime, CancellationToken cancellationToken);
        public  Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        public  Task<bool> AddBookToUserAsync(User user, CancellationToken cancellationToken);
        public  Task<List<string>> GetUserBooks(string username, CancellationToken cancellationToken);


    }
}
