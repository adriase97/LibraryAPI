﻿using Microsoft.Extensions.Configuration;
using Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Core.Services
{
    public class TokenService : ITokenService
    {
        #region Fields
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Constructor
        public TokenService(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        #endregion

        #region Public Methods
        public async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expireMinutes = int.Parse(jwtSettings["ExpireMinutes"]);

            // Get user roles and claims
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            // Create a list of claims
            var tokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token ID
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                tokenClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Adding additional user claims
            tokenClaims.AddRange(claims);

            var key = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claimsIdentity = new ClaimsIdentity(tokenClaims, JwtBearerDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
