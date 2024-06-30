using BookStore.Data.Abstraction;
using BookStore.Domain;
using BookStore.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabase database;
        private readonly IMongoCollection<User> users;

        public UserRepository(IDatabase database)
        {
            this.database = database;
            users = database.Getcollection<IMongoCollection<User>, User>("Users");
        }

        public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, id);
            var user = await this.users.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return user;
        }

        public async Task<string> InsertAsync(User user, CancellationToken cancellationToken)
        {
            await this.users.InsertOneAsync(user, new InsertOneOptions(), cancellationToken);
            return user.Id;
        }

        public Task<bool> UdpateAsync(User item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByNameAsync(string username, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(User  => User.Name, username);

            var user = await users.Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (user != null)
            {
                user.Id = user.Id.ToString(); 
            }

            return user;        
        }


        public async Task UpdateRefreshTokenAsync(string userId, string refreshToken, DateTime refreshTokenExpiryTime, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update
                .Set(u => u.RefreshToken, refreshToken)
                .Set(u => u.RefreshTokenExpiryTime, refreshTokenExpiryTime);

            await users.UpdateOneAsync(filter, update, null, cancellationToken);
        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            return await users.Find(u => u.RefreshToken == refreshToken).SingleOrDefaultAsync(cancellationToken);
        }


        public async Task<bool> AddBookToUserAsync(User user, CancellationToken cancellationToken)
        {
       

            var filter = Builders<User>.Filter.Eq(user => user.Id, user.Id);
            var updateResult = await this.users.ReplaceOneAsync(filter, user, new ReplaceOptions(), cancellationToken);
            return updateResult.ModifiedCount > 0;

        }

        public async Task<List<string>> GetUserBooks(string username, CancellationToken cancellationToken)
        {
            {
                var filter = Builders<User>.Filter.Eq(u => u.Name, username);
                var user = await users.Find(filter).FirstOrDefaultAsync();

                var  books = user.Books.ToList();
                return books;
            }
        }


    }
}