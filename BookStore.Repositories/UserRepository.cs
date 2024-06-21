using BookStore.Data.Abstraction;
using BookStore.Domain;
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

        public Task<string> InsertAsync(User item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
    }
}