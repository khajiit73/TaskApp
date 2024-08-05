using System.Security.Claims;
using TaskApp.Services.Interfaces;

namespace TaskApp.Api.Services
{
    public class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
    {

        private readonly HttpContext _context = accessor.HttpContext!;

        public string UserId => _context!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
