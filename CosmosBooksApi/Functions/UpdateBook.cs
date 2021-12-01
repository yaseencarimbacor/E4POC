using e4POCApi.Models;
using e4POCApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace e4POCApi.Functions
{
    public class UpdateBook
    {
        private readonly ILogger<UpdateBook> _logger;
        private readonly IBookService _bookService;

        public UpdateBook(
            ILogger<UpdateBook> logger,
            IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [FunctionName(nameof(UpdateBook))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Book/{id}")] HttpRequest req,
            string id)
        {
            IActionResult result;

            try
            {
                var bookToUpdate = await _bookService.GetBook(id);

                if (bookToUpdate == null)
                {
                    _logger.LogWarning($"Book with id: {id} doesn't exist.");
                    result = new StatusCodeResult(StatusCodes.Status404NotFound);
                }

                var input = await new StreamReader(req.Body).ReadToEndAsync();

                var updateBookRequest = JsonConvert.DeserializeObject<Book>(input);

                Book updatedBook = new Book
                {
                    Id = id,
                    FirstName = updateBookRequest.FirstName,
                    Surname = updateBookRequest.Surname,
                    Gender = updateBookRequest.Gender,
                    Email = updateBookRequest.Email,
                    TelephoneNumber = updateBookRequest.TelephoneNumber,
                    City = updateBookRequest.City,
                    DateOfBirth = updateBookRequest.DateOfBirth,
                    Notes = updateBookRequest.Notes
                };

                await _bookService.UpdateBook(id, updatedBook);

                result = new StatusCodeResult(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error: {ex.Message}");
                result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return result;
        }
    }
}
