using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.AppUsers.Commands
{
    public class LoginHandller(
        IMapper mapper,
        IAppUserReposatory userrepo,
        IJwtReposatory JWTrepo,
        IRefreshTokenReposatory refreshTokenRepo)
        : IRequestHandler<LoginCommand, AppUserDTO>
    {
        public async Task<AppUserDTO> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user =
                await userrepo.GetByEmail(request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid Email or Password");
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException(
                    "Invalid Email or Password");
            }

            // Generate Access Token
            var token =
                JWTrepo.GenerateToken(user);

            // Generate Refresh Token
            var refreshToken =
                JWTrepo.GenerateRefreshToken();

            // Save Refresh Token in Database
            var refreshTokenEntity =
                new Smart_Logistics_Mangment_System.Domain.Models.RefreshToken
                {
                    Token = refreshToken,
                    ExpiryDate = DateTime.UtcNow.AddDays(7),
                    IsRevoked = false,
                    RevokedAt = null,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

            await refreshTokenRepo.Add(refreshTokenEntity);
            await refreshTokenRepo.Save();

            // Map User
            var result =
                mapper.Map<AppUserDTO>(user);

            // Add Tokens
            result.Token = token;
            result.RefreshToken = refreshToken;

            return result;
        }
    }
}