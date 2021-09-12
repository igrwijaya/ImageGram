using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ImageGram.Core.Application.Services;
using ImageGram.Core.Application.Services.Response;
using ImageGram.Core.Constant.Constant;
using ImageGram.Core.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ImageGram.Core.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        #region Fields

        private readonly SignInManager<AppIdentityModel> _signInManager;
        private readonly UserManager<AppIdentityModel> _userManager;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructors

        public IdentityService(
            UserManager<AppIdentityModel> userManager, 
            SignInManager<AppIdentityModel> signInManager, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        #endregion

        #region IIdentityService Members

        public async Task<IdentityResponse> CreateAsync(string name, string email, string password, string role)
        {
            var response = new IdentityResponse();
            try
            {
                var user = new AppIdentityModel
                {
                    Name = name,
                    Email = email
                };

                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    response.AddErrorMessages(result.Errors.Select(item => item.Description));

                    return response;
                }

                await _userManager.AddToRoleAsync(user, role);

                response.UserId = user.Id;
            }
            catch (Exception e)
            {
                response.AddErrorMessage(e.Message);
            }

            return response;
        }

        public async Task<IdentityResponse> LoginAsync(string email, string password, string expectedRole)
        {
            var response = new IdentityResponse();
            var loginResult = await _signInManager.PasswordSignInAsync(email, password, false, false);
            
            if (!loginResult.Succeeded)
            {
                response.ErrorMessages.Add("Invalid email or password");
                return response;
            }

            var user = await _userManager.FindByEmailAsync(email);

            var isCorrectRole = await _userManager.IsInRoleAsync(user, expectedRole);
            if (!isCorrectRole)
            {
                response.ErrorMessages.Add("Invalid Access");
                return response;
            }

            response.UserId = user.Id;
            response.Token = GenerateToken(user);

            return response;
        }

        #endregion
        
        #region Private Methods

        private string GenerateToken(AppIdentityModel user)
        {
            var claims = new List<Claim>();
            var customClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id)
            };

            claims.AddRange(customClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[ConfigurationConstant.JwtKey]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
        
    }
}