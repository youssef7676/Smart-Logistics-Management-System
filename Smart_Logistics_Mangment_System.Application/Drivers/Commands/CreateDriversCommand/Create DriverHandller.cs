using MediatR;
using Smart_Logistics_Mangment_System.Application.Drivers.Commands.CreateDriversCommand;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

public class CreateDriverHandler(
    IDriverReposatory driverRepo,
    IAppUserReposatory userRepo,
    IVehicleReposatory vehicleRepo)
    : IRequestHandler<CreateDriverCommand, int>
{
    public async Task<int> Handle(
        CreateDriverCommand request,
        CancellationToken cancellationToken)
    {
        // Check if email already exists
        var users = await userRepo.GetAll();

        var emailExists = users.Any(x =>
            x.Email.ToLower() == request.Email.ToLower());

        if (emailExists)
        {
            throw new Exception("Email already exists.");
        }

        // Find available vehicle
        var vehicleId = await vehicleRepo.GetAvailableVehicleId();

        // Create AppUser
        var user = new AppUser
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = UserRole.Driver,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await userRepo.Add(user);
        await userRepo.Save();

        // Create Driver
        var driver = new Driver
        {
            UserId = user.Id,
            LicenseNumber = request.LicenseNumber,
            VehicleId = vehicleId == 0 ? null : vehicleId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await driverRepo.Add(driver);
        await driverRepo.Save();

        return driver.Id;
    }
}