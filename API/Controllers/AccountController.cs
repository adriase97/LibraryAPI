using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [EnableCors("CorsPolicy")]
    [Route("libraryApi/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        #endregion

        #region Constructor
        public AccountController(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        #endregion

        #region Get
        [Authorize(Roles = "Admin")]
        [HttpGet("All")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(new { message = "ok", response = users });
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfileAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return NotFound("User not found");

            return Ok(new { message = "ok", response = new { user.Id, user.Email, user.UserName } });
        }
        #endregion

        #region Post
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new IdentityUser { UserName = registerUserDto.UserName, Email = registerUserDto.Email };
            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "ok" });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDto.Password)) return Unauthorized("Invalid login attempt.");

            var token = await _tokenService.GenerateTokenAsync(user);

            return Ok(new { message = "ok", response = token });
        }
        #endregion

        #region Put
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound("User not found");

            var result = await _userManager.ChangePasswordAsync(user, changePasswordUserDto.CurrentPassword, changePasswordUserDto.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "ok" });
        }
        #endregion

        #region Delete
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
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
