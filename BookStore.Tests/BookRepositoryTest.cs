using BookStore.Data.Abstraction;
using BookStore.Data.MongoDB;
using BookStore.Domain;
using BookStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests
{
    public class BookRepositoryTest :IDisposable
    {
        private IDatabaseConfiguration databaseConfiguration;
        private IDatabase database;
        private IBookRepository bookRepository; 
        
        public BookRepositoryTest() {
            databaseConfiguration = new DatabaseConfiguration
            {
                ConnectionString = "mongodb+srv://konczhunor:onyCe7MDVVGL993X@cluster0.nrb8aem.mongodb.net/",
                DatabaseName = "BookStore"
            };
            database = new Database(databaseConfiguration);
            bookRepository = new BookRepository(database);

        }

        [Fact]
        public async Task ShouldSaveAndGetBook()
        {
            List<string> genres = new List<string>() { 
            "comedy", "humor"};
            Book newBook = new Book { Id = "6492b5d2751145c2f50afb47",
                Title = "Carte de bucate",
                YearOfPublication = new DateTime(), Genres = genres };
            await this.bookRepository.InsertAsync(newBook, CancellationToken.None);

            var foundBook = await this.bookRepository.GetByIdAsync("6492b5d2751145c2f50afb47", CancellationToken.None);
            Assert.Equal(newBook.Title, foundBook.Title);
        }

        [Fact]
        public async Task GetBook()
        {
            var foundBook = await this.bookRepository.GetByIdAsync("6492b5d2751145c2f50afb47", CancellationToken.None);
            Assert.NotNull(foundBook);
        }


        public void Dispose()
        {

        }
    }
}
