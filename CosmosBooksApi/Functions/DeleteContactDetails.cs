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
    public class DeleteContactDetails
    {
        private readonly ILogger<DeleteContactDetails> _logger;
        private readonly IContactDetailsService _bookService;

        public DeleteContactDetails(
            ILogger<DeleteContactDetails> logger,
            IContactDetailsService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [FunctionName(nameof(DeleteContactDetails))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Book/{id}")] HttpRequest req,
            string id)
        {
            IActionResult result;

            try
            {
                var bookToDelete = await _bookService.GetBook(id);

                if (bookToDelete == null)
                {
                    _logger.LogWarning($"Book with id: {id} doesn't exist.");
                    result = new StatusCodeResult(StatusCodes.Status404NotFound);
                }

                await _bookService.RemoveContactDetails(bookToDelete);
                result = new StatusCodeResult(StatusCodes.Status204NoContent);
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
