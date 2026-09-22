using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using Smart_Logistics_Mangment_System.Application.RefreshTokenss.Commands;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.Application.RefreshToken
{
    public class RefreshTokenHandler(
        IRefreshTokenReposatory refreshTokenRepo,
        IAppUserReposatory userRepo,
        IJwtReposatory jwtRepo,
        IMapper mapper)
        : IRequestHandler<RefreshTokenCommand, AppUserDTO>
    {
        public async Task<AppUserDTO> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var refreshToken =
                await refreshTokenRepo.GetByRefreshToken(
                    request.RefreshToken);

            if (refreshToken == null ||
                refreshToken.IsDeleted ||
                refreshToken.IsRevoked)
            {
                throw new UnauthorizedAccessException(
                    "Invalid refresh token.");
            }

            if (refreshToken.ExpiryDate <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException(
                    "Refresh token has expired.");
            }

            var user =
                await userRepo.GetById(refreshToken.UserId);

            if (user == null || user.IsDeleted)
            {
                throw new UnauthorizedAccessException(
                    "User not found.");
            }

            // Revoke old refresh token
            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            refreshTokenRepo.Update(refreshToken);

            // Generate new tokens
            var newAccessToken =
                jwtRepo.GenerateToken(user);

            var newRefreshToken =
                jwtRepo.GenerateRefreshToken();

            var newRefreshTokenEntity =
                new Domain.Models.RefreshToken
                {
                    Token = newRefreshToken,
                    ExpiryDate = DateTime.UtcNow.AddDays(7),
                    IsRevoked = false,
                    RevokedAt = null,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

            await refreshTokenRepo.Add(
                newRefreshTokenEntity);

            await refreshTokenRepo.Save();

            var result =
                mapper.Map<AppUserDTO>(user);

            result.Token = newAccessToken;
            result.RefreshToken = newRefreshToken;

            return result;
        }
    }
}