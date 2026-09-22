//using MediatR;
//using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;
//using Smart_Logistics_Mangment_System.Domain.Enums;
//using Smart_Logistics_Mangment_System.Domain.Models;
//using Smart_Logistics_Mangment_System.Domain.Interfaces;

//namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipmentStatus
//{
//    public class UpdateShipmentStatusHandler(
//        IShipmentReposatory shiprepo,
//        IShipmentTrackingReposatory trackrepo,
//        IDriverReposatory driverrepo,
//        ICurrentUserService currentUserService)
//        : IRequestHandler<UpdateShipmentStatusCommand, ShipmentTrackingDTO>
//    {
//        public async Task<ShipmentTrackingDTO> Handle(
//            UpdateShipmentStatusCommand request,
//            CancellationToken cancellationToken)
//        {
//            // ============================================
//            // Get Shipment
//            // ============================================

//            var shipment =
//                await shiprepo.GetById(request.ShipmentId);

//            if (shipment == null || shipment.IsDeleted)
//            {
//                throw new KeyNotFoundException(
//                    "Shipment not found.");
//            }


//            // ============================================
//            // Driver Authorization
//            // ============================================

//            if (currentUserService.Role == "Driver")
//            {
//                var driver =
//                    await driverrepo.GetDriverByUserId(
//                        currentUserService.UserId);

//                if (driver == null)
//                {
//                    throw new UnauthorizedAccessException(
//                        "Driver profile not found.");
//                }

//                if (shipment.DriverId != driver.Id)
//                {
//                    throw new UnauthorizedAccessException(
//                        "You are not allowed to update this shipment.");
//                }
//            }


//            // ============================================
//            // Check Valid Status Transition
//            // ============================================

//            bool validTransition = shipment.Status switch
//            {
//                ShipmentStatus.Created =>
//                    request.Status == ShipmentStatus.Assigned ||
//                    request.Status == ShipmentStatus.Cancelled,

//                ShipmentStatus.Assigned =>
//                    request.Status == ShipmentStatus.PickedUp ||
//                    request.Status == ShipmentStatus.Cancelled,

//                ShipmentStatus.PickedUp =>
//                    request.Status == ShipmentStatus.InTransit ||
//                    request.Status == ShipmentStatus.Cancelled,

//                ShipmentStatus.InTransit =>
//                    request.Status == ShipmentStatus.Delivered ||
//                    request.Status == ShipmentStatus.Cancelled,

//                ShipmentStatus.Delivered => false,

//                ShipmentStatus.Cancelled => false,

//                _ => false
//            };


//            if (!validTransition)
//            {
//                throw new InvalidOperationException(
//                    $"Cannot change shipment status from " +
//                    $"{shipment.Status} to {request.Status}.");
//            }


//            // ============================================
//            // Update Shipment Status
//            // ============================================

//            shipment.Status = request.Status;


//            // ============================================
//            // Picked Up Date
//            // ============================================

//            if (request.Status == ShipmentStatus.PickedUp)
//            {
//                shipment.PickedUpAt = DateTime.UtcNow;
//            }


//            // ============================================
//            // Delivered Date
//            // ============================================

//            if (request.Status == ShipmentStatus.Delivered)
//            {
//                shipment.DeliveredAt = DateTime.UtcNow;
//            }


//            // ============================================
//            // Update Shipment
//            // ============================================

//            shiprepo.Update(shipment);


//            // ============================================
//            // Create Shipment Tracking Automatically
//            // ============================================

//            var tracking = new ShipmentTracking
//            {
//                ShipmentId = shipment.Id,
//                Status = request.Status,
//                Location = request.Location,
//                Note = request.Note
//            };

//            await trackrepo.Add(tracking);


//            // ============================================
//            // Save Both Changes
//            // ============================================

//            await shiprepo.Save();


//            // ============================================
//            // Return Tracking
//            // ============================================

//            return new ShipmentTrackingDTO
//            {
//                Id = tracking.Id,
//                ShipmentId = tracking.ShipmentId,
//                Status = tracking.Status,
//                Location = tracking.Location,
//                Note = tracking.Note
//            };
//        }
//    }
//}




using MediatR;
using Smart_Logistics_Mangment_System.Application.Notifications;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipmentStatus
{
    public class UpdateShipmentStatusHandler(
        IShipmentReposatory shiprepo,
        IShipmentTrackingReposatory trackrepo,
        IDriverReposatory driverrepo,
        ICurrentUserService currentUserService,
        INotificationService notificationService)
        : IRequestHandler<UpdateShipmentStatusCommand, ShipmentTrackingDTO>
    {
        public async Task<ShipmentTrackingDTO> Handle(
            UpdateShipmentStatusCommand request,
            CancellationToken cancellationToken)
        {
            // Get shipment with details
            var shipment =
                await shiprepo.GetShipmentByIdWithDetails(
                    request.ShipmentId);

            if (shipment == null || shipment.IsDeleted)
                throw new KeyNotFoundException(
                    "Shipment not found.");

            // Driver permission
            if (currentUserService.Role == "Driver")
            {
                var driver =
                    await driverrepo.GetDriverByUserId(
                        currentUserService.UserId);

                if (driver == null)
                    throw new UnauthorizedAccessException(
                        "Driver profile not found.");

                if (shipment.DriverId != driver.Id)
                    throw new UnauthorizedAccessException(
                        "You are not allowed to update this shipment.");
            }

            // Validate status transition
            bool validTransition = shipment.Status switch
            {
                ShipmentStatus.Created =>
                    request.Status == ShipmentStatus.Assigned ||
                    request.Status == ShipmentStatus.Cancelled,

                ShipmentStatus.Assigned =>
                    request.Status == ShipmentStatus.PickedUp ||
                    request.Status == ShipmentStatus.Cancelled,

                ShipmentStatus.PickedUp =>
                    request.Status == ShipmentStatus.InTransit ||
                    request.Status == ShipmentStatus.Cancelled,

                ShipmentStatus.InTransit =>
                    request.Status == ShipmentStatus.Delivered ||
                    request.Status == ShipmentStatus.Cancelled,

                ShipmentStatus.Delivered => false,

                ShipmentStatus.Cancelled => false,

                _ => false
            };

            if (!validTransition)
                throw new InvalidOperationException(
                    $"Cannot change shipment status from {shipment.Status} to {request.Status}.");

            // Update shipment status
            shipment.Status = request.Status;

            if (request.Status == ShipmentStatus.PickedUp)
                shipment.PickedUpAt = DateTime.UtcNow;

            if (request.Status == ShipmentStatus.Delivered)
                shipment.DeliveredAt = DateTime.UtcNow;

            shiprepo.Update(shipment);

            // Create tracking automatically
            var tracking = new ShipmentTracking
            {
                ShipmentId = shipment.Id,
                Status = request.Status,
                Location = request.Location,
                Note = request.Note
            };

            await trackrepo.Add(tracking);

            // Save changes
            await shiprepo.Save();

            // SignalR notification to assigned driver
            if (shipment.DriverId.HasValue)
            {
                var driver =
                    await driverrepo.GetDriverById(
                        shipment.DriverId.Value);

                if (driver != null)
                {
                    await notificationService.SendToUserAsync(
                        driver.UserId,
                        $"Shipment {shipment.Id} status changed to {request.Status}.");
                }
            }

            // SignalR notification to customer
            if (shipment.Customer != null)
            {
                await notificationService.SendToUserAsync(
                    shipment.Customer.UserId,
                    $"Shipment {shipment.Id} status changed to {request.Status}.");
            }

            // Return tracking DTO
            return new ShipmentTrackingDTO
            {
                Id = tracking.Id,
                ShipmentId = tracking.ShipmentId,
                Status = tracking.Status,
                Location = tracking.Location,
                Note = tracking.Note
            };
        }
    }
}