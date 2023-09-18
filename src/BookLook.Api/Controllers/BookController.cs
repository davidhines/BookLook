using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookLook.Application.Book.Queries;

namespace BookLook.Api.Controllers
{
    [Route("api/book")]
    [ApiController]
    public sealed class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var result = await _mediator.Send(new GetBook(id));

            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
