using BookStore.Data.Abstraction;
using BookStore.Domain;
using BookStore.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IDatabase database;
        private readonly IMongoCollection<Book> books;

        
        public BookRepository(IDatabase database) {
            this.database = database;
            books = database.Getcollection<IMongoCollection<Book>, Book>("Books");
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<Book>.Filter.Eq(book => book.Id, id);
                await this.books.DeleteOneAsync(filter, cancellationToken);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

            public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken)
        {

            //var bookList = await books.Find(_ => true).ToListAsync(cancellationToken);

            //return bookList; //  not working 
            // Error: Cannot deserialize a 'String' from BsonType 'ObjectId'.

            var filter = Builders<Book>.Filter.Empty;
            var result = await this.books.Find(filter).Limit(10).ToListAsync(cancellationToken);
            return result;

        }

        public async Task<Book> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var objectId = new ObjectId(id);

            var filter = Builders<Book>.Filter.Eq(book => book.Id,id);
            var book = await this.books.Find(filter).FirstAsync(cancellationToken);
            return book;
        }

        public async Task<string> InsertAsync(Book item, CancellationToken cancellationToken)
        {

            await this.books.InsertOneAsync(item, new InsertOneOptions(), cancellationToken);
            return item.Id;
        }

        public async Task<bool> UdpateAsync(Book item, CancellationToken cancellationToken)
        {
            var filter = Builders<Book>.Filter.Eq(book => book.Id, item.Id);
            var updateResult = await this.books.ReplaceOneAsync(filter, item, new ReplaceOptions(), cancellationToken);
            return updateResult.ModifiedCount > 0;
        }
    }
}
