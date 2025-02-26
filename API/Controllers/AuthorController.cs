using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, ViewAuthorsBooks")]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                if (User.HasClaim("AuthorsAccess", "false")) return Forbid();

                var authors = await _authorService.GetAllAsync();
                return Ok(new { message = "ok", response = authors });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, ViewAuthorsBooks")]
        [HttpGet("AllWithIncludes")]
        public async Task<IActionResult> GetAllWithIncludesAsync()
        {
            try
            {
                if (User.HasClaim("AuthorsAccess", "false")) return Forbid();

                var authors = await _authorService.GetAllWithIncludesAsync();
                return Ok(new { message = "ok", response = authors });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, ViewAuthorsBooks")]
        [HttpGet("Specification")]
        public async Task<IActionResult> GetBySpecificationAsync(string? name)
        {
            try
            {
                if (User.HasClaim("AuthorsAccess", "false")) return Forbid();

                var authors = await _authorService.GetBySpecificationAsync(name);
                return Ok(new { message = "ok", response = authors });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, ViewAuthorsBooks")]
        [HttpGet("SpecificationWithIncludes")]
        public async Task<IActionResult> GetBySpecificationWithIncludesAsync(string? name)
        {
            try
            {
                if (User.HasClaim("AuthorsAccess", "false")) return Forbid();

                var authors = await _authorService.GetBySpecificationWithIncludesAsync(name);
                return Ok(new { message = "ok", response = authors });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, ViewAuthorsBooks")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                if (User.HasClaim("AuthorsAccess", "false")) return Forbid();

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

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, ViewAuthorsBooks")]
        [HttpGet("WithIncludes/{id:int}")]
        public async Task<IActionResult> GetByIdWithIncludesAsync(int id)
        {
            try
            {
                if (User.HasClaim("AuthorsAccess", "false")) return Forbid();

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
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks")]
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                if (User.HasClaim("AuthorsCreate", "false")) return Forbid();

                if (!ModelState.IsValid) return BadRequest(ModelState);

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
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                if (User.HasClaim("AuthorsEdit", "false")) return Forbid();

                if (!ModelState.IsValid) return BadRequest(ModelState);

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
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (User.HasClaim("AuthorsDelete", "false")) return Forbid();

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
