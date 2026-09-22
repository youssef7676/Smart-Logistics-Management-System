using System.Security.Claims;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.API.Services
{
    public class CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
        : ICurrentUserService
    {
        public int UserId
        {
            get
            {
                var userId =
                    httpContextAccessor.HttpContext?
                        .User
                        .FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException(
                        "User is not authenticated.");

                return int.Parse(userId);
            }
        }

        public string Role
        {
            get
            {
                var role =
                    httpContextAccessor.HttpContext?
                        .User
                        .FindFirstValue(ClaimTypes.Role);

                if (string.IsNullOrEmpty(role))
                    throw new UnauthorizedAccessException(
                        "User role not found.");

                return role;
            }
        }
    }
}