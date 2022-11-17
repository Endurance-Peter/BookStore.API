using BookStore.API.Model;
using BookStore.API.Repository;
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
    public class BooksController : ControllerBase
    {
        public IBookRepository BookRepository { get; set; }
        public BooksController(IBookRepository repository)
        {
            BookRepository = repository;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllBooks()//([FromServices] IBookRepository repository)
        {
            var result = await BookRepository.GetAllBooksAsync();

            return Ok(result);
        }
        [HttpPost("")]
        public IActionResult AddBooks([FromBody] BookModel model)//, [FromServices] IBookRepository repository)
        {
            BookRepository.AddBookAsync(model);

            return Ok();
        }
        [HttpGet("get-books-by-id")]
        public async Task<IActionResult> GetBookById([FromRoute] int bookId)//, [FromServices] IBookRepository repository)
        {
            var result = await BookRepository.GetBookByIdAsync(bookId);

            if (result==null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
