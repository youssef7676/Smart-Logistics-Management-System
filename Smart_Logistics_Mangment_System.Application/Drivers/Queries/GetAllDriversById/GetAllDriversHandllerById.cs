using AutoMapper;
using MediatR;

using Smart_Logistics_Mangment_System.Application.Drivers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.Drivers.Queries.GetAllDriversById
{
    public class GetAllDriversHandllerById(
        IDriverReposatory driverepo,
        ICurrentUserService currentUserService,
        IMapper mapper)
        : IRequestHandler<GetAllDriversQueriesById, DriversDTO>
    {
        public async Task<DriversDTO> Handle(
            GetAllDriversQueriesById request,
            CancellationToken cancellationToken)
        {
            var role = currentUserService.Role;
            var userId = currentUserService.UserId;

            Driver driver =
                await driverepo.GetAllDriversById(
                    request.Id,
                    "User",
                    "Vehicle");

            if (driver == null)
                return null;

            // Admin + Employee
            if (role == "Admin" || role == "Employee")
            {
                return mapper.Map<DriversDTO>(driver);
            }

            // Driver → himself only
            if (role == "Driver")
            {
                if (driver.UserId != userId)
                {
                    throw new UnauthorizedAccessException(
                        "You are not allowed to view this driver.");
                }

                return mapper.Map<DriversDTO>(driver);
            }

            throw new UnauthorizedAccessException(
                "You are not allowed to view drivers.");
        }
    }
}