using e4POCApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace e4POCApi.Functions
{
    public class GetContactDetailsById
    {
        private readonly ILogger<GetContactDetailsById> _logger;
        private readonly IContactDetailsService _bookService;

        public GetContactDetailsById(
            ILogger<GetContactDetailsById> logger,
            IContactDetailsService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [FunctionName(nameof(GetContactDetailsById))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Book/{id}")] HttpRequest req,
            string id)
        {
            IActionResult result;

            try
            {
                var book = await _bookService.GetBook(id);

                if (book == null)
                {
                    _logger.LogWarning($"Book with id: {id} doesn't exist.");
                    result = new StatusCodeResult(StatusCodes.Status404NotFound);
                }

                result = new OkObjectResult(book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error. Exception thrown: {ex.Message}");
                result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return result;
        }
    }
}
