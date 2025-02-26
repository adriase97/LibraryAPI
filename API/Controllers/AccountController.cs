using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("libraryApi/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        #endregion

        #region Constructor
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        #endregion

        #region Get
        [HttpGet("all-users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(u => new { u.Id, u.Email }).ToList();
            return Ok(new { message = "ok", response = users });
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfileAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return NotFound("User not found");

            return Ok(new { message = "ok", response = new { user.Id, user.Email } });
        }
        #endregion

        #region Post
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new IdentityUser { UserName = registerUserDto.Email, Email = registerUserDto.Email };
            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "ok" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDto.Password)) return Unauthorized("Invalid login attempt.");

            return Ok(new { message = "ok" });
        }
        #endregion

        #region Put
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return NotFound("User not found");

            user.Email = userDto.Email ?? user.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "ok" });
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordUserDto changePasswordUserDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound("User not found");

            var result = await _userManager.ChangePasswordAsync(user, changePasswordUserDto.CurrentPassword, changePasswordUserDto.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "ok" });
        }
        #endregion

        #region Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "ok" });
        }
        #endregion
        
    }
}
