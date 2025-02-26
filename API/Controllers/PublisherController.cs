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
    public class PublisherController : Controller
    {
        #region Fields
        private readonly IPublisherService _publisherService;
        #endregion

        #region Constructor
        public PublisherController(IPublisherService publisherService) => this._publisherService = publisherService;
        #endregion

        #region Get
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, Publisher")]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                if (User.HasClaim("PublishersAccess", "false")) return Forbid();

                var publishers = await _publisherService.GetAllAsync();
                return Ok(new { message = "ok", response = publishers });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, Publisher")]
        [HttpGet("AllWithIncludes")]
        public async Task<IActionResult> GetAllWithIncludesAsync()
        {
            try
            {
                if (User.HasClaim("PublishersAccess", "false")) return Forbid();

                var publishers = await _publisherService.GetAllWithIncludesAsync();
                return Ok(new { message = "ok", response = publishers });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, Publisher")]
        [HttpGet("Specification")]
        public async Task<IActionResult> GetBySpecificationAsync(string? name, string? country)
        {
            try
            {
                if (User.HasClaim("PublishersAccess", "false")) return Forbid();

                var publishers = await _publisherService.GetBySpecificationAsync(name, country);
                return Ok(new { message = "ok", response = publishers });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, Publisher")]
        [HttpGet("SpecificationWithIncludes")]
        public async Task<IActionResult> GetBySpecificationWithIncludesAsync(string? name, string? country)
        {
            try
            {
                if (User.HasClaim("PublishersAccess", "false")) return Forbid();

                var publishers = await _publisherService.GetBySpecificationWithIncludesAsync(name, country);
                return Ok(new { message = "ok", response = publishers });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, Publisher")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                if (User.HasClaim("PublishersAccess", "false")) return Forbid();

                var publisher = await _publisherService.GetByIdAsync(id);
                return Ok(new { message = "ok", response = publisher });
            }
            catch (PublisherException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, AuthorsBooksPublisher, Publisher")]
        [HttpGet("WithIncludes/{id:int}")]
        public async Task<IActionResult> GetByIdWithIncludesAsync(int id)
        {
            try
            {
                if (User.HasClaim("PublishersAccess", "false")) return Forbid();

                var publisher = await _publisherService.GetByIdWithIncludesAsync(id);
                return Ok(new { message = "ok", response = publisher });
            }
            catch (PublisherException ex)
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
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, Publisher")]
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] PublisherDTO publisherDTO)
        {
            try
            {
                if (User.HasClaim("PublishersCreate", "false")) return Forbid();

                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _publisherService.AddAsync(publisherDTO);
                return Ok(new { message = "ok" });
            }
            catch (PublisherException ex)
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
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, Publisher")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] PublisherDTO publisherDTO)
        {
            try
            {
                if (User.HasClaim("PublishersEdit", "false")) return Forbid();

                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _publisherService.UpdateAsync(publisherDTO);
                return Ok(new { message = "ok" });
            }
            catch (PublisherException ex)
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
        [Authorize(Roles = "Admin, AuthorsBooksPublisher, Publisher")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (User.HasClaim("PublishersDelete", "false")) return Forbid();

                await _publisherService.DeleteAsync(id);
                return Ok(new { message = "ok" });
            }
            catch (PublisherException ex)
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
