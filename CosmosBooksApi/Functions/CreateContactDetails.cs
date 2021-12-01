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
    public class CreateContactDetails
    {
        private readonly ILogger<CreateContactDetails> _logger;
        private readonly IContactDetailsService _bookService;

        public CreateContactDetails(
            ILogger<CreateContactDetails> logger,
            IContactDetailsService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [FunctionName(nameof(CreateContactDetails))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ContactDetails")] HttpRequest req)
        {
            IActionResult result;

            try
            {
                var incomingRequest = await new StreamReader(req.Body).ReadToEndAsync();

                var bookRequest = JsonConvert.DeserializeObject<ContactDetails>(incomingRequest);

                var book = new ContactDetails
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

                await _bookService.CreateContactDetails(book);

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
