using AutoMapper;
using MediatR;

using Smart_Logistics_Mangment_System.Application.Drivers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.Drivers.Commands.UpdateDriversCommand
{
    public class UpdateDriverHandller(
        IDriverReposatory driverepo,
        ICurrentUserService currentUserService,
        IMapper mapper)
        : IRequestHandler<UpdateDriverCommand, DriversDTO>
    {
        public async Task<DriversDTO> Handle(
            UpdateDriverCommand request,
            CancellationToken cancellationToken)
        {
            var role = currentUserService.Role;
            var userId = currentUserService.UserId;

            // Only Admin, Employee and Driver
            if (role != "Admin" &&
                role != "Employee" &&
                role != "Driver")
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to update drivers.");
            }

            Driver driver =
                await driverepo.GetDriverById(request.Id);

            if (driver == null)
                return null;

            // Driver can update himself only
            if (role == "Driver")
            {
                if (driver.UserId != userId)
                {
                    throw new UnauthorizedAccessException(
                        "You are not allowed to update this driver.");
                }
            }

            driver.LicenseNumber = request.LicenseNumber;

            if (driver.User != null)
            {
                driver.User.FullName = request.FullName;
            }

            driverepo.Update(driver);

            await driverepo.Save();

            return mapper.Map<DriversDTO>(driver);
        }
    }
}