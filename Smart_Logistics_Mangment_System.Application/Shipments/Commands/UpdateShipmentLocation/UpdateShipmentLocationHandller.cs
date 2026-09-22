using MediatR;
using Smart_Logistics_Mangment_System.Application.Notifications;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipmentLocation
{
    public class UpdateShipmentLocationHandler(
        IShipmentReposatory shiprepo,
        IDriverReposatory driverrepo,
        ICurrentUserService currentUserService,
        IETAService etaService,
        INotificationService notificationService)
        : IRequestHandler<
            UpdateShipmentLocationCommand,
            ShipmentETADTO>
    {
        public async Task<ShipmentETADTO> Handle(
            UpdateShipmentLocationCommand request,
            CancellationToken cancellationToken)
        {
            var shipment =
                await shiprepo.GetShipmentByIdWithDetails(
                    request.ShipmentId);

            if (shipment == null || shipment.IsDeleted)
            {
                throw new KeyNotFoundException(
                    "Shipment not found.");
            }

            if (currentUserService.Role != "Driver")
            {
                throw new UnauthorizedAccessException(
                    "Only drivers can update shipment location.");
            }

            var driver =
                await driverrepo.GetDriverByUserId(
                    currentUserService.UserId);

            if (driver == null)
            {
                throw new UnauthorizedAccessException(
                    "Driver profile not found.");
            }

            if (shipment.DriverId != driver.Id)
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to update this shipment location.");
            }

            if (shipment.Status != ShipmentStatus.Assigned &&
                shipment.Status != ShipmentStatus.PickedUp &&
                shipment.Status != ShipmentStatus.InTransit)
            {
                throw new InvalidOperationException(
                    $"Cannot update location while shipment status is {shipment.Status}.");
            }

            if (request.Latitude < -90 ||
                request.Latitude > 90)
            {
                throw new ArgumentException(
                    "Latitude must be between -90 and 90.");
            }

            if (request.Longitude < -180 ||
                request.Longitude > 180)
            {
                throw new ArgumentException(
                    "Longitude must be between -180 and 180.");
            }

            if (!shipment.DestinationLatitude.HasValue ||
                !shipment.DestinationLongitude.HasValue)
            {
                throw new InvalidOperationException(
                    "Shipment destination coordinates are not available.");
            }

            var location = new ShipmentLocation
            {
                ShipmentId = shipment.Id,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                RecordedAt = DateTime.UtcNow
            };

            await shiprepo.AddShipmentLocation(location);

            var distanceKm =
                etaService.CalculateDistanceKm(
                    request.Latitude,
                    request.Longitude,
                    shipment.DestinationLatitude.Value,
                    shipment.DestinationLongitude.Value);

            var estimatedMinutes =
                etaService.CalculateEstimatedMinutes(
                    distanceKm);

            await shiprepo.Save();

            var estimatedTime =
                FormatEstimatedTime(
                    estimatedMinutes);

            if (shipment.Customer != null)
            {
                await notificationService.SendToUserAsync(
                    shipment.Customer.UserId,
                    new
                    {
                        type = "ShipmentLocationUpdated",
                        shipmentId = shipment.Id,
                        latitude = request.Latitude,
                        longitude = request.Longitude,
                        distanceKm = Math.Round(
                            distanceKm,
                            2),
                        estimatedMinutes,
                        estimatedTime
                    });
            }

            return new ShipmentETADTO
            {
                ShipmentId = shipment.Id,

                CurrentLatitude =
                    request.Latitude,

                CurrentLongitude =
                    request.Longitude,

                DestinationLatitude =
                    shipment.DestinationLatitude.Value,

                DestinationLongitude =
                    shipment.DestinationLongitude.Value,

                DistanceKm =
                    Math.Round(
                        distanceKm,
                        2),

                EstimatedMinutes =
                    estimatedMinutes,

                EstimatedTime =
                    estimatedTime
            };
        }

        private static string FormatEstimatedTime(
            int minutes)
        {
            if (minutes <= 0)
                return "Arrived";

            int hours = minutes / 60;

            int remainingMinutes =
                minutes % 60;

            if (hours == 0)
                return $"{remainingMinutes}m";

            if (remainingMinutes == 0)
                return $"{hours}h";

            return $"{hours}h {remainingMinutes}m";
        }
    }
}
