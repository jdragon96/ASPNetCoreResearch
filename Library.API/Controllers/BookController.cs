using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using System.Text;
using System.Text.Json;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return await _repository.GetAll(u => u.Id);
        }

        [HttpGet]
        public async Task<ActionResult<BookSelectResponse>> GetBooks([FromQuery] int page)
        {
            BookSelectResponse res = new BookSelectResponse
            {
                Books = await _repository.GetAll(u => u.Id, page, 10),
                NumOfBook = _repository.Rows()
            };
            return res;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await _repository.Get(u => u.Id == id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book user)
        {
            var findUser = await _repository.Get(u => u.Id == id);
            if (findUser == null)
            {
                return BadRequest();
            }
            _repository.Update(user);
            _repository.Save();
            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            _repository.Add(book);
            _repository.Save();
            return CreatedAtAction("PostBook", new { id = book.Id }, book);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook([FromQuery] int id)
        {
            if(_repository == null)
                return NotFound();

            var user = await _repository.Get(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _repository.Remove(user);
            _repository.Save();
            return NoContent();
        }
    }
}
