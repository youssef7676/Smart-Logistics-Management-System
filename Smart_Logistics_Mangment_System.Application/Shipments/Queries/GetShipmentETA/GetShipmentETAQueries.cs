using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Queries.GetShipmentETA
{
    public class GetShipmentETAHandler(
        IShipmentReposatory shiprepo,
        IETAService etaService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            GetShipmentETAQuery,
            ShipmentETADTO>
    {
        public async Task<ShipmentETADTO> Handle(
            GetShipmentETAQuery request,
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

            if (currentUserService.Role == "Customer")
            {
                if (shipment.Customer == null ||
                    shipment.Customer.UserId !=
                    currentUserService.UserId)
                {
                    throw new UnauthorizedAccessException(
                        "You are not allowed to view this shipment.");
                }
            }

            if (currentUserService.Role == "Driver")
            {
                var driver =
                    await shiprepo.GetDriverByUserId(
                        currentUserService.UserId);

                if (driver == null ||
                    shipment.DriverId != driver.Id)
                {
                    throw new UnauthorizedAccessException(
                        "You are not allowed to view this shipment.");
                }
            }

            if (!shipment.DestinationLatitude.HasValue ||
                !shipment.DestinationLongitude.HasValue)
            {
                throw new InvalidOperationException(
                    "Shipment destination coordinates are not available.");
            }

            var location =
                await shiprepo.GetLastShipmentLocation(
                    shipment.Id);

            if (location == null)
            {
                throw new InvalidOperationException(
                    "No location has been recorded for this shipment yet.");
            }

            var distanceKm =
                etaService.CalculateDistanceKm(
                    location.Latitude,
                    location.Longitude,
                    shipment.DestinationLatitude.Value,
                    shipment.DestinationLongitude.Value);

            var estimatedMinutes =
                etaService.CalculateEstimatedMinutes(
                    distanceKm);

            return new ShipmentETADTO
            {
                ShipmentId =
                    shipment.Id,

                CurrentLatitude =
                    location.Latitude,

                CurrentLongitude =
                    location.Longitude,

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
                    FormatEstimatedTime(
                        estimatedMinutes)
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