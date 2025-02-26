using Core.DTOs;
using Core.Exceptions;
using Core.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var publishers = await _publisherService.GetAllAsync();
                return Ok(new { message = "ok", response = publishers });
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
                var publishers = await _publisherService.GetAllWithIncludesAsync();
                return Ok(new { message = "ok", response = publishers });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Specification")]
        public async Task<IActionResult> GetBySpecificationAsync(string? name, string? country)
        {
            try
            {
                var publishers = await _publisherService.GetBySpecificationAsync(name, country);
                return Ok(new { message = "ok", response = publishers });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SpecificationWithIncludes")]
        public async Task<IActionResult> GetBySpecificationWithIncludesAsync(string? name, string? country)
        {
            try
            {
                var publishers = await _publisherService.GetBySpecificationWithIncludesAsync(name, country);
                return Ok(new { message = "ok", response = publishers });
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
        [HttpGet("WithIncludes/{id:int}")]
        public async Task<IActionResult> GetByIdWithIncludesAsync(int id)
        {
            try
            {
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
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] PublisherDTO publisherDTO)
        {
            try
            {
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
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] PublisherDTO publisherDTO)
        {
            try
            {
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
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
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
