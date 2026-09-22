using AutoMapper;
using MediatR;

using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.UpdateShipmentRequest
{
    public class UpdateShipmentRequestHandller(
        IShipmentItemReposatory itemrepo,
        IShipmentRequestItemReposatory reqrepo,
        IMapper mapper,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            UpdateShipmentRequestCommand,
            ShipmentRequestDTO>
    {
        public async Task<ShipmentRequestDTO> Handle(
            UpdateShipmentRequestCommand request,
            CancellationToken cancellationToken)
        {
            var userId = currentUserService.UserId;
            var role = currentUserService.Role;

            // Get Shipment Request with its Items and Customer
            var shipmentRequest =
                await reqrepo.GetByIdWithDetails(request.Id);

            if (shipmentRequest == null)
            {
                return null;
            }

            // ==========================================
            // Authorization
            // ==========================================

            if (role == "Customer")
            {
                if (shipmentRequest.Customer == null)
                {
                    throw new KeyNotFoundException(
                        "Customer not found.");
                }

                // Customer can only update his own request
                if (shipmentRequest.Customer.UserId != userId)
                {
                    throw new UnauthorizedAccessException(
                        "You are not allowed to modify this shipment request.");
                }

                // Customer can only update Pending requests
                if (shipmentRequest.Status != ShipmentRequestStatus.Pending)
                {
                    throw new InvalidOperationException(
                        "Customer can only update pending shipment requests.");
                }
            }
            else if (role != "Admin" && role != "Employee")
            {
                throw new UnauthorizedAccessException(
                    "You are not authorized to modify shipment requests.");
            }

            // ==========================================
            // Admin / Employee Rules
            // ==========================================

            if ((role == "Admin" || role == "Employee") &&
                shipmentRequest.ShipmentId.HasValue)
            {
                throw new InvalidOperationException(
                    "Cannot update a shipment request after shipment creation.");
            }

            // ==========================================
            // Update Request Information
            // ==========================================

            shipmentRequest.PickupWarehouseId =
                request.PickupWarehouseId;

            shipmentRequest.DestinationAddress =
                request.DestinationAddress;

            reqrepo.Update(shipmentRequest);

            // ==========================================
            // Update / Add / Delete Items
            // ==========================================

            var requestItemIds = request.Items
                .Where(x => x.Id != 0)
                .Select(x => x.Id)
                .ToHashSet();

            // Delete existing items that were removed
            // from the update request
            var itemsToDelete = shipmentRequest.Items
                .Where(x => !requestItemIds.Contains(x.Id))
                .ToList();

            foreach (var item in itemsToDelete)
            {
                await itemrepo.DeleteFromUpdate(item.Id);
            }

            // Add new items / Update existing items
            foreach (var item in request.Items)
            {
                // Existing Item
                if (item.Id != 0)
                {
                    var existingItem =
                        shipmentRequest.Items
                            .FirstOrDefault(x => x.Id == item.Id);

                    if (existingItem == null)
                    {
                        throw new KeyNotFoundException(
                            $"Shipment Item with Id {item.Id} does not belong to this Shipment Request.");
                    }

                    existingItem.ItemName =
                        item.ItemName;

                    existingItem.Quantity =
                        item.Quantity;

                    existingItem.Weight =
                        item.Weight;

                    existingItem.Description =
                        item.Description;

                    itemrepo.Update(existingItem);
                }
                else
                {
                    // New Item
                    var newItem = new ShipmentItem
                    {
                        ItemName = item.ItemName,
                        Quantity = item.Quantity,
                        Weight = item.Weight,
                        Description = item.Description,
                        ShipmentRequestId = shipmentRequest.Id
                    };

                    await itemrepo.Add(newItem);
                }
            }

            // ==========================================
            // Save Changes
            // ==========================================

            await reqrepo.Save();
            await itemrepo.Save();

            // ==========================================
            // Get Updated Request
            // ==========================================

            var updatedRequest =
                await reqrepo.GetByIdWithDetails(request.Id);

            if (updatedRequest == null)
            {
                throw new KeyNotFoundException(
                    "Shipment Request not found after update.");
            }

            return mapper.Map<ShipmentRequestDTO>(
                updatedRequest);
        }
    }
}