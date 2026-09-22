using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Domain.Interfaces
{
    public interface IJwtReposatory
    {
        string GenerateToken(AppUser user);

        string GenerateRefreshToken();
    }
}
