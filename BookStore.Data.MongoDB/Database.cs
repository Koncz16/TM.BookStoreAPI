﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Abstraction;
using BookStore.Domain;
using BookStore.Domain.DTOs;
using BookStore.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace BookStore.Data.MongoDB
{
    public class Database : IDatabase
    {
        private readonly IMongoDatabase db;
        private readonly IMongoClient client;

        public Database(IDatabaseConfiguration configuration)
        {
            this.RegisterCustomMappings();

            this.client = new MongoClient(configuration.ConnectionString);
            this.db = this.client.GetDatabase(configuration.DatabaseName);

            //this.client = new MongoClient("mongodb+srv://konczhunor:onyCe7MDVVGL993X@cluster0.nrb8aem.mongodb.net/");

            //this.db = this.client.GetDatabase("BookStore");

        }

        public TCollection Getcollection<TCollection, TItem>(string Name) where TCollection : class
        {
            return this.db.GetCollection<TItem>(Name) as TCollection;
        }

        private void RegisterCustomMappings()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Book)))
            {
                BsonClassMap.RegisterClassMap<Book>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(book => book.PublisherId)
                    .SetIdGenerator(new StringObjectIdGenerator())
                    .SetIdGenerator(new ObjectIdGenerator())
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                    cm.MapMember(book => book.AuthorId)
                    .SetIdGenerator(new StringObjectIdGenerator())
                    .SetIdGenerator(new ObjectIdGenerator())
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                    cm.MapIdMember(book => book.Id)
                    .SetIdGenerator(new StringObjectIdGenerator())
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                    cm.SetIgnoreExtraElements(true);
                });
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                BsonClassMap.RegisterClassMap<User>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(user => user.Id)
                      .SetIdGenerator(StringObjectIdGenerator.Instance)
                      .SetSerializer(new StringSerializer(BsonType.ObjectId));
                    cm.SetIgnoreExtraElements(true);
                });
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(BookOutDto)))
            {
                BsonClassMap.RegisterClassMap<BookOutDto>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(book => book.Id)
                      .SetIdGenerator(StringObjectIdGenerator.Instance)
                      .SetSerializer(new StringSerializer(BsonType.ObjectId));
                    cm.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(AuthorDto)))
            {
                BsonClassMap.RegisterClassMap<AuthorDto>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(author => author.Name)
                      .SetSerializer(new StringSerializer(BsonType.String));
                    cm.SetIgnoreExtraElements(true);
                });
            }



            }




        //private void RegisterCustomMappings()
        //{
        //    if (!BsonClassMap.IsClassMapRegistered(typeof(Book)))
        //    {
        //        BsonClassMap.RegisterClassMap<Book>(cm =>
        //        {
        //            cm.AutoMap();
        //            cm.MapMember(book => book.PublisherId)
        //             .SetIdGenerator(new StringObjectIdGenerator())
        //             .SetIdGenerator(new ObjectIdGenerator())
        //             .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //            cm.MapMember(book => book.AuthorId)
        //            .SetIdGenerator(new StringObjectIdGenerator())
        //            .SetIdGenerator(new ObjectIdGenerator())
        //            .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //            cm.MapIdMember(book => book.Id).
        //            SetIdGenerator(new StringObjectIdGenerator()).
        //            SetSerializer(new StringSerializer(BsonType.ObjectId));
        //            cm.SetIgnoreExtraElements(true);
        //        });
        //    }
        //    //if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
        //    //{
        //    //    BsonClassMap.RegisterClassMap<User>(cm =>
        //    //    {
        //    //        cm.AutoMap();
        //    //        cm.MapIdMember(user => user.Id)
        //    //          .SetIdGenerator(new StringObjectIdGenerator())
        //    //          .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //    //        cm.SetIgnoreExtraElements(true);
        //    //    });
        //    //}
        //}
    }
}
