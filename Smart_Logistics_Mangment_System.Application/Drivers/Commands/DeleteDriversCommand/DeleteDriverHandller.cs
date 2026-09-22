using AutoMapper;
using MediatR;

using Smart_Logistics_Mangment_System.Application.Drivers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.Drivers.Commands.DeleteDriversCommand
{
    public class DeleteDriverHandller(
        IDriverReposatory driverepo,
        ICurrentUserService currentUserService,
        IMapper mapper)
        : IRequestHandler<DeleteDriverCommand, DriversDTO>
    {
        public async Task<DriversDTO> Handle(
            DeleteDriverCommand request,
            CancellationToken cancellationToken)
        {
            var role = currentUserService.Role;

            // Admin only
            if (role != "Admin")
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to delete drivers.");
            }

            Driver driver =
                await driverepo.GetById(request.Id);

            if (driver == null)
                return null;

            await driverepo.Delete(driver.Id);
            await driverepo.Save();

            return mapper.Map<DriversDTO>(driver);
        }
    }
}