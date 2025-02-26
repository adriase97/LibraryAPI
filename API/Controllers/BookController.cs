using Core.DTOs;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("libraryApi/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        #region Fields
        private readonly IBookService _bookService;
        #endregion

        #region Constructor
        public BookController(IBookService bookService) => this._bookService = bookService;
        #endregion

        #region Get
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var books = await _bookService.GetAllAsync();
                return Ok(new { message = "ok", response = books });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("AllWithIncludes")]
        public async Task<IActionResult> GetAllWithIncludesAsync()
        {
            try
            {
                var books = await _bookService.GetAllWithIncludesAsync();
                return Ok(new { message = "ok", response = books });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Specification")]
        public async Task<IActionResult> GetBySpecificationAsync(string? title, Genre? genre, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                var books = await _bookService.GetBySpecificationAsync(title, genre, minPrice, maxPrice);
                return Ok(new { message = "ok", response = books });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SpecificationWithIncludes")]
        public async Task<IActionResult> GetBySpecificationWithIncludesAsync(string? title, Genre? genre, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                var books = await _bookService.GetBySpecificationWithIncludesAsync(title, genre, minPrice, maxPrice);
                return Ok(new { message = "ok", response = books });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var book = await _bookService.GetByIdAsync(id);
                return Ok(new { message = "ok", response = book });
            }
            catch (BookException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("WithIncludes/{id:int}")]
        public async Task<IActionResult> GetByIdWithIncludesAsync(int id)
        {
            try
            {
                var book = await _bookService.GetByIdWithIncludesAsync(id);
                return Ok(new { message = "ok", response = book });
            }
            catch (BookException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Post
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] BookDTO bookDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _bookService.AddAsync(bookDTO);
                return Ok(new { message = "ok" });
            }
            catch (BookException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Put
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] BookDTO bookDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _bookService.UpdateAsync(bookDTO);
                return Ok(new { message = "ok" });
            }
            catch (BookException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _bookService.DeleteAsync(id);
                return Ok(new { message = "ok" });
            }
            catch (BookException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
