using AutoMapper;
using MediatR;

using Smart_Logistics_Mangment_System.Application.Drivers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.Drivers.Queries.GetAllDrivers
{
    public class GetAllDriversHandller(
        IDriverReposatory driverepo,
        ICurrentUserService currentUserService,
        IMapper mapper)
        : IRequestHandler<GetAllDriversQueries, List<DriversDTO>>
    {
        public async Task<List<DriversDTO>> Handle(
            GetAllDriversQueries request,
            CancellationToken cancellationToken)
        {
            var role = currentUserService.Role;
            var userId = currentUserService.UserId;

            List<Driver> drivers =
                await driverepo.GetAllDrivers(
                    "User",
                    "Vehicle");

            // Admin + Employee
            if (role == "Admin" || role == "Employee")
            {
                return mapper.Map<List<DriversDTO>>(drivers);
            }

            // Driver → himself only
            if (role == "Driver")
            {
                var myDriver = drivers
                    .Where(x => x.UserId == userId)
                    .ToList();

                return mapper.Map<List<DriversDTO>>(myDriver);
            }

            throw new UnauthorizedAccessException(
                "You are not allowed to view drivers.");
        }
    }
}