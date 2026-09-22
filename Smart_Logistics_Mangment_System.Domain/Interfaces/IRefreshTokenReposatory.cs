using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Domain.Interfaces
{
    public interface IRefreshTokenReposatory : IReposatory<RefreshToken>
    {
        Task<RefreshToken?> GetByRefreshToken(string refreshToken);
    }
}