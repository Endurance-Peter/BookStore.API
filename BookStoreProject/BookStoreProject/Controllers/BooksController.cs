using BookStore.API.Model;
using BookStore.API.Repository;
using BookStoreProject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("books")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBooks([FromServices] IBookRepository repository)
        {
            var result = await repository.GetAllBooksAsync();

            return Ok(result);
        }
        [HttpPost("")]
        public async Task<IActionResult> AddBooks([FromBody] BookModel model, [FromServices] IBookRepository repository)
        {
            var result = await repository.AddBookAsync(model);

            return Ok(result);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetBookById([FromRoute] int bookId, [FromServices] IBookRepository repository)
        {
            var result = await repository.GetBookByIdAsync(bookId);

            if (result==null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, [FromBody] BookModel model, [FromServices] IBookRepository repository)
        {
            await repository.UpdateBookAsync(bookId, model);

            return Ok();
        }
        
    }
}
