using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Notifications;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.AssignShipment
{
    public class AssignShipmentHandller(
        IShipmentReposatory shiprepo,
        IMapper mapper,
        INotificationService notificationService)
        : IRequestHandler<AssignShipmentCommand, ShipmentDTO>
    {
        public async Task<ShipmentDTO> Handle(
            AssignShipmentCommand request,
            CancellationToken cancellationToken)
        {
            // Get shipment with details
            var shipment =
                await shiprepo.GetShipmentByIdWithDetails(
                    request.ShipmentId);

            if (shipment == null)
            {
                throw new KeyNotFoundException(
                    "Shipment not found.");
            }

            // Only Created shipments can be assigned
            if (shipment.Status != ShipmentStatus.Created)
            {
                throw new InvalidOperationException(
                    "Only Created shipments can be assigned.");
            }

            // Get driver
            var driver =
                await shiprepo.GetDriverById(
                    request.DriverId);

            if (driver == null)
            {
                throw new KeyNotFoundException(
                    "Driver not found.");
            }

            // Driver must have a vehicle
            if (!driver.VehicleId.HasValue)
            {
                throw new InvalidOperationException(
                    "This driver does not have a vehicle.");
            }

            // Check if driver already has an active shipment
            var driverShipment =
                await shiprepo.GetActiveShipmentByDriverId(
                    request.DriverId);

            if (driverShipment != null)
            {
                throw new InvalidOperationException(
                    $"Driver is already assigned to Shipment {driverShipment.Id}.");
            }

            // Check if vehicle already has an active shipment
            var vehicleShipment =
                await shiprepo.GetActiveShipmentByVehicleId(
                    driver.VehicleId.Value);

            if (vehicleShipment != null)
            {
                throw new InvalidOperationException(
                    $"Vehicle is already assigned to Shipment {vehicleShipment.Id}.");
            }

            // Assign driver and vehicle
            shipment.DriverId = driver.Id;
            shipment.VehicleId = driver.VehicleId.Value;
            shipment.Status = ShipmentStatus.Assigned;

            shiprepo.Update(shipment);

            // Save assignment
            await shiprepo.Save();

            // Notification to Driver
            await notificationService.SendToUserAsync(
                driver.UserId,
                $"Shipment {shipment.Id} has been assigned to you.");

            // Notification to Customer
            if (shipment.Customer != null)
            {
                await notificationService.SendToUserAsync(
                    shipment.Customer.UserId,
                    $"Shipment {shipment.Id} has been assigned to a driver.");
            }

            // Get updated shipment
            var updatedShipment =
                await shiprepo.GetShipmentByIdWithDetails(
                    shipment.Id);

            if (updatedShipment == null)
            {
                throw new KeyNotFoundException(
                    "Shipment was assigned but could not be retrieved.");
            }

            return mapper.Map<ShipmentDTO>(
                updatedShipment);
        }
    }
}