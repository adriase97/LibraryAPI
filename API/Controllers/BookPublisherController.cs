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
    public class BookPublisherController : Controller
    {
        #region Fields
        private readonly IBookPublisherService _bookPublisherService;
        #endregion

        #region Constructor
        public BookPublisherController(IBookPublisherService bookPublisherService) => this._bookPublisherService = bookPublisherService;
        #endregion

        #region Get
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                if (User.HasClaim("BooksAccess", "false") || User.HasClaim("PublishersAccess", "false")) return Forbid();

                var bookPublishers = await _bookPublisherService.GetAllAsync();
                return Ok(new { message = "ok", response = bookPublishers });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{bookId:int}/{publisherId:int}")]
        public async Task<IActionResult> GetByIdAsync(int bookId, int publisherId)
        {
            try
            {
                if (User.HasClaim("BooksAccess", "false") || User.HasClaim("PublishersAccess", "false")) return Forbid();

                var bookPublisher = await _bookPublisherService.GetByIdAsync(bookId, publisherId);
                return Ok(new { message = "ok", response = bookPublisher });
            }
            catch (BookPublisherException ex)
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
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, Publisher")]
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] BookPublisherDTO bookPublisherDTO)
        {
            try
            {
                if (User.HasClaim("BooksCreate", "false") || User.HasClaim("BooksEdit", "false") ||
                    User.HasClaim("PublishersCreate", "false") || User.HasClaim("PublishersEdit", "false")) return Forbid();

                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _bookPublisherService.AddAsync(bookPublisherDTO);
                return Ok(new { message = "ok" });
            }
            catch (BookPublisherException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, Publisher")]
        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRangeAsync([FromBody] IEnumerable<BookPublisherDTO> bookPublisherDTOs)
        {
            try
            {
                if (User.HasClaim("BooksCreate", "false") || User.HasClaim("BooksEdit", "false") ||
                    User.HasClaim("PublishersCreate", "false") || User.HasClaim("PublishersEdit", "false")) return Forbid();

                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _bookPublisherService.AddRangeAsync(bookPublisherDTOs);
                return Ok(new { message = "ok" });
            }
            catch (BookPublisherException ex)
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
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, Publisher")]
        [HttpDelete("Delete/{bookId:int}/{publisherId:int}")]
        public async Task<IActionResult> DeleteAsync(int bookId, int publisherId)
        {
            try
            {
                if (User.HasClaim("BooksEdit", "false") || User.HasClaim("PublishersEdit", "false")) return Forbid();

                await _bookPublisherService.DeleteAsync(bookId, publisherId);
                return Ok(new { message = "ok" });
            }
            catch (BookPublisherException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, AuthorsBooks, Publisher")]
        [HttpDelete("DeleteByBookOrPublisher")]
        public async Task<IActionResult> DeleteByBookOrPublisherAsync(int? bookId = null, int? publisherId = null)
        {
            try
            {
                if (User.HasClaim("BooksEdit", "false") || User.HasClaim("PublishersEdit", "false")) return Forbid();

                await _bookPublisherService.DeleteByBookOrPublisherAsync(bookId, publisherId);
                return Ok(new { message = "ok" });
            }
            catch (BookPublisherException ex)
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
