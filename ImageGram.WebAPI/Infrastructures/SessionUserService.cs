using System.Security.Claims;
using ImageGram.Core.Application.Services;
using Microsoft.AspNetCore.Http;

namespace ImageGram.WebAPI.Infrastructures
{
    public class SessionUserService : ISessionUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);
        
        public string UserEmail => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
    }
}