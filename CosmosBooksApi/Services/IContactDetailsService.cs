using e4POCApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e4POCApi.Services
{
    public interface IContactDetailsService
    {
        /// <summary>
        /// Get all books from the Books collection
        /// </summary>
        /// <returns></returns>
        Task<List<ContactDetails>> GetBooks();

        /// <summary>
        /// Get a book by its id from the Books collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ContactDetails> GetBook(string id);

        /// <summary>
        /// Insert a book into the Books collection
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Task CreateContactDetails(ContactDetails bookIn);

        /// <summary>
        /// Updates an existing book in the Books collection
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        Task UpdateContactDetails(string id, ContactDetails bookIn);

        /// <summary>
        /// Removes a book from the Books collection
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Task RemoveContactDetails(ContactDetails bookIn);

        /// <summary>
        /// Removes a book with the specified id from the Books collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveContactDetailsById(string id);
    }
}
