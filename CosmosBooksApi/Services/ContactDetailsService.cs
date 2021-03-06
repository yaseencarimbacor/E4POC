using e4POCApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e4POCApi.Services
{
    public class ContactDetailsService : IContactDetailsService
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ContactDetails> _books;

        public ContactDetailsService(
            MongoClient mongoClient,
            IConfiguration configuration)
        {
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(configuration["DatabaseName"]);
            _books = _database.GetCollection<ContactDetails>(configuration["CollectionName"]);
        }

        public async Task CreateContactDetails(ContactDetails bookIn)
        {
            await _books.InsertOneAsync(bookIn);
        }

        public async Task<ContactDetails> GetBook(string id)
        {
            var book = await _books.FindAsync(book => book.Id == id);
            return book.FirstOrDefault();
        }

        public async Task<List<ContactDetails>> GetBooks()
        {
            var books = await _books.FindAsync(book => true);
            return books.ToList();
        }

        public async Task RemoveContactDetails(ContactDetails bookIn)
        {
            await _books.DeleteOneAsync(book => book.Id == bookIn.Id);
        }

        public async Task RemoveContactDetailsById(string id)
        {
            await _books.DeleteOneAsync(book => book.Id == id);
        }

        public async Task UpdateContactDetails(string id, ContactDetails bookIn)
        {
            await _books.ReplaceOneAsync(book => book.Id == id, bookIn);
        }
    }
}
