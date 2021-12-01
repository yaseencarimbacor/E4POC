using e4POCApi.Models;
using e4POCApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace e4POCApi.Functions
{
    public class CreateBook
    {
        private readonly ILogger<CreateBook> _logger;
        private readonly IBookService _bookService;

        public CreateBook(
            ILogger<CreateBook> logger,
            IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [FunctionName(nameof(CreateBook))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Book")] HttpRequest req)
        {
            IActionResult result;

            try
            {
                var incomingRequest = await new StreamReader(req.Body).ReadToEndAsync();

                var bookRequest = JsonConvert.DeserializeObject<Book>(incomingRequest);

                var book = new Book
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    FirstName = bookRequest.FirstName,
                    Surname = bookRequest.Surname,
                    Gender = bookRequest.Gender,
                    Email = bookRequest.Email,
                    TelephoneNumber = bookRequest.TelephoneNumber,
                    City = bookRequest.City,
                    DateOfBirth = bookRequest.DateOfBirth,
                    Notes = bookRequest.Notes

                };

                await _bookService.CreateBook(book);

                result = new StatusCodeResult(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error. Exception: {ex.Message}");
                result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return result;
        }
    }
}
