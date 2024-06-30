using BookStore.Data.Abstraction;
using BookStore.Domain;
using BookStore.Domain.DTOs;
using BookStore.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
namespace BookStore.Repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly IDatabase database;
        private readonly IMongoCollection<Book> books;
        private readonly IMongoCollection<Author    > authors;


        public HomeworkRepository(IDatabase database)
        {
            this.database = database;
            books = database.Getcollection<IMongoCollection<Book>, Book>("Books");
            authors = database.Getcollection<IMongoCollection<Author>, Author>("Authors");
        }

        public async Task<List<Book>> GetBooksByAuthorNationality(CancellationToken cancellationToken)
        {
            var pipeline = new List<BsonDocument>
            {
                new BsonDocument("$lookup",
                new BsonDocument
                    {
                        { "from", "Authors" },
                        { "localField", "AuthorId" },
                        { "foreignField", "_id" },
                        { "as", "Authors" }
                    }),
                new BsonDocument("$unwind",
                new BsonDocument("path", "$Authors")),
                new BsonDocument("$match",
                new BsonDocument("Authors.Nationality", "Colombia")),
                new BsonDocument("$limit", 100) };

            var aggregateOptions = new AggregateOptions { AllowDiskUse = true };

            var aggregation = await books.AggregateAsync<Book>(pipeline, aggregateOptions, cancellationToken);

            return await aggregation.ToListAsync(cancellationToken);
        }

        public async Task<List<Book>> GetRecentBooks(CancellationToken cancellationToken)
        {
            var pipeline = new List<BsonDocument>
    {
        new BsonDocument("$addFields",
        new BsonDocument
            {
                { "CurrentYear",
        new BsonDocument("$year", DateTime.Now) },
                { "PublicationYear",
        new BsonDocument("$year", "$YearOfPublication") }
            }),
        new BsonDocument("$match",
        new BsonDocument("$expr",
        new BsonDocument("$lte",
        new BsonArray
                    {
                        new BsonDocument("$subtract",
                        new BsonArray
                            {
                                "$CurrentYear",
                                "$PublicationYear"
                            }),
                        2
                    }))),
        new BsonDocument("$addFields",
        new BsonDocument("IsNew", true)),
        new BsonDocument("$project",
        new BsonDocument
            {
                { "CurrentYear", 0 },
                { "PublicationYear", 0 }
            }),
        new BsonDocument("$limit", 100)

            };

            var aggregateOptions = new AggregateOptions { AllowDiskUse = true };

            var aggregation = await books.AggregateAsync<Book>(pipeline, aggregateOptions, cancellationToken);

            return await aggregation.ToListAsync(cancellationToken);
        }

        public async Task<List<BookOutDto>> GetBooksByGenresAndAddAuthorName(CancellationToken cancellationToken)
        {
            var pipeline = new List<BsonDocument>
    {
        new BsonDocument("$match",
            new BsonDocument("Genres",
                new BsonDocument("$all",
                    new BsonArray { "Humor", "Essay" }))),
        new BsonDocument("$lookup",
            new BsonDocument
            {
                { "from", "Authors" },
                { "localField", "AuthorId" },
                { "foreignField", "_id" },
                { "as", "Authors" }
            }),
        new BsonDocument("$unwind", new BsonDocument("path", "$Authors")),
        new BsonDocument("$addFields",
            new BsonDocument("Authors.Name",
                new BsonDocument("$concat",
                    new BsonArray { "$Authors.FirstName", " ", "$Authors.LastName" }))),
        new BsonDocument("$project",
            new BsonDocument
            {
                { "Authors.FirstName", 0 },
                { "Authors.LastName", 0 }
            }),
            new BsonDocument("$limit", 100)

    };

            var aggregateOptions = new AggregateOptions { AllowDiskUse = true }; 

            var result = await books.AggregateAsync<BsonDocument>(pipeline, aggregateOptions, cancellationToken);

            var bookList = new List<BookOutDto>();

            await result.ForEachAsync(doc =>
            {
                var book = new BookOutDto
                {
                    Id = doc["_id"].AsObjectId.ToString(),
                    Title= doc["Title"].AsString.ToString(),
                    Genres = doc["Genres"].AsBsonArray.Select(g => g.AsString).ToList(),
                    Author = new AuthorDto
                    {
                        Name = doc["Authors"]["Name"].AsString
                    }
                };
                bookList.Add(book);
            }, cancellationToken);

            return bookList;
        }
     }


}

