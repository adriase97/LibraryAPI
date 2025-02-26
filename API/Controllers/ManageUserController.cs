using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("libraryApi/[controller]")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Constructor
        public ManageUserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Get
        [HttpGet("Roles")]
        public async Task<IActionResult> GetRolesAsync()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound("User not found");

                var roles = await _userManager.GetRolesAsync(user);
                return Ok(new { message = "ok", response = roles });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Claims")]
        public async Task<IActionResult> GetClaimsAsync()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound("User not found");

                var claims = await _userManager.GetClaimsAsync(user);
                return Ok(new { message = "ok", response = claims });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Post

        [HttpPost("AddRole/{role}")]
        public async Task<IActionResult> AddToRoleAsync(string role)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound("User not found");

                if (!await _roleManager.RoleExistsAsync(role))
                    return BadRequest(new { message = "Role does not exist" });

                var result = await _userManager.AddToRoleAsync(user, role);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Failed to add role", errors = result.Errors });

                return Ok(new { message = "ok" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddToRolesAsync([FromBody] IEnumerable<string> roles)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound("User not found");

                var invalidRoles = roles.Where(role => !_roleManager.RoleExistsAsync(role).Result).ToList();
                if (invalidRoles.Any())
                    return BadRequest(new { message = "Some roles do not exist", invalidRoles });

                var result = await _userManager.AddToRolesAsync(user, roles);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Failed to add role", errors = result.Errors });

                return Ok(new { message = "ok" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("AddClaim")]
        public async Task<IActionResult> AddClaimAsync([FromBody] ClaimDTO claimDTO)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound(new { message = "User not found" });

                

                var existingClaims = await _userManager.GetClaimsAsync(user);
                if (existingClaims.Any(c => c.Type == claimDTO.Type && c.Value == claimDTO.Value))
                    return BadRequest(new { message = "Claim already exists" });

                var claim = new Claim(claimDTO.Type, claimDTO.Value);

                var result = await _userManager.AddClaimAsync(user, claim);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Failed to add claim", errors = result.Errors });

                return Ok(new { message = "ok" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("AddClaims")]
        public async Task<IActionResult> AddClaimsAsync([FromBody] List<ClaimDTO> claims)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound(new { message = "User not found" });

                var existingClaims = await _userManager.GetClaimsAsync(user);

                var newClaims = claims
                    .Where(c => !existingClaims.Any(ec => ec.Type == c.Type && ec.Value == c.Value))
                    .Select(c => new Claim(c.Type, c.Value))
                    .ToList();

                if (!newClaims.Any())
                    return BadRequest(new { message = "All claims already exist" });

                var result = await _userManager.AddClaimsAsync(user, newClaims);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Failed to add claims", errors = result.Errors });

                return Ok(new { message = "ok" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        #endregion

        #region Put
        [HttpPut("ReplaceClaim")]
        public async Task<IActionResult> ReplaceClaimAsync([FromBody] ReplaceClaimDTO replaceClaimDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound(new { message = "User not found" });

                var oldClaim = new Claim(replaceClaimDto.OldType, replaceClaimDto.OldValue);
                var newClaim = new Claim(replaceClaimDto.NewType, replaceClaimDto.NewValue);

                var result = await _userManager.ReplaceClaimAsync(user, oldClaim, newClaim);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Failed to replace claim", errors = result.Errors });

                return Ok(new { message = "Claim replaced successfully" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        #endregion

        #region Delete
        [HttpDelete("RemoveRole/{role}")]
        public async Task<IActionResult> RemoveFromRoleAsync(string role)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound(new { message = "User not found" });

                // Verificar si el rol existe
                if (!await _roleManager.RoleExistsAsync(role))
                    return BadRequest(new { message = "Role does not exist" });

                // Verificar si el usuario tiene el rol antes de eliminarlo
                if (!await _userManager.IsInRoleAsync(user, role))
                    return BadRequest(new { message = "User is not in this role" });

                var result = await _userManager.RemoveFromRoleAsync(user, role);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Failed to remove role", errors = result.Errors });

                return Ok(new { message = "ok" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("RemoveRoles")]
        public async Task<IActionResult> RemoveFromRolesAsync([FromBody] IEnumerable<string> roles)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound(new { message = "User not found" });

                // Obtener los roles existentes para el usuario
                var userRoles = await _userManager.GetRolesAsync(user);

                // Verificar que los roles existen en el sistema
                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                        return BadRequest(new { message = $"Role '{role}' does not exist" });

                    if (!userRoles.Contains(role))
                        return BadRequest(new { message = $"User is not in role '{role}'" });
                }

                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Failed to remove roles", errors = result.Errors });

                return Ok(new { message = "ok" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("RemoveClaim")]
        public async Task<IActionResult> RemoveClaimAsync([FromBody] ClaimDTO claimDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound(new { message = "User not found" });

                var claim = new Claim(claimDto.Type, claimDto.Value);

                var result = await _userManager.RemoveClaimAsync(user, claim);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Failed to remove claim", errors = result.Errors });

                return Ok(new { message = "Claim removed successfully" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("RemoveClaims")]
        public async Task<IActionResult> RemoveClaimsAsync([FromBody] List<ClaimDTO> claimsDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user == null) return NotFound(new { message = "User not found" });

                var claims = claimsDto.Select(c => new Claim(c.Type, c.Value)).ToList();

                var result = await _userManager.RemoveClaimsAsync(user, claims);
                if (!result.Succeeded)
                    return BadRequest(new { message = "Failed to remove claims", errors = result.Errors });

                return Ok(new { message = "Claims removed successfully" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        #endregion
    }
}
