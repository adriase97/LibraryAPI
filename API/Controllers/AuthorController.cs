using Core.DTOs;
using Core.Exceptions;
using Core.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("libraryApi/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        #region Fields
        private readonly IAuthorService _authorService;
        #endregion

        #region Constructor
        public AuthorController(IAuthorService authorService) => this._authorService = authorService;
        #endregion

        #region Get
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var authors = await _authorService.GetAllAsync();
                return Ok(new { message = "ok", response = authors });
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
                var authors = await _authorService.GetAllWithIncludesAsync();
                return Ok(new { message = "ok", response = authors });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Specification")]
        public async Task<IActionResult> GetBySpecificationAsync(string? name)
        {
            try
            {
                var authors = await _authorService.GetBySpecificationAsync(name);
                return Ok(new { message = "ok", response = authors });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SpecificationWithIncludes")]
        public async Task<IActionResult> GetBySpecificationWithIncludesAsync(string? name)
        {
            try
            {
                var authors = await _authorService.GetBySpecificationWithIncludesAsync(name);
                return Ok(new { message = "ok", response = authors });
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
                var author = await _authorService.GetByIdAsync(id);
                return Ok(new { message = "ok", response = author });
            }
            catch (AuthorException ex)
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
                var author = await _authorService.GetByIdWithIncludesAsync(id);
                return Ok(new { message = "ok", response = author });
            }
            catch (AuthorException ex)
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
        public async Task<IActionResult> AddAsync([FromBody]AuthorDTO authorDTO)
        {
            try
            {
                await _authorService.AddAsync(authorDTO);
                return Ok(new { message = "ok" });
            }
            catch (AuthorException ex)
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
        public async Task<IActionResult> UpdateAsync([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                await _authorService.UpdateAsync(authorDTO);
                return Ok(new { message = "ok" });
            }
            catch (AuthorException ex)
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
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _authorService.DeleteAsync(id);
                return Ok(new { message = "ok" });
            }
            catch (AuthorException ex)
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
