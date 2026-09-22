using AutoMapper;
using MediatR;

using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands
{
    public class CreateShipmentRequestsHandller(
        IShipmentItemReposatory itemrepo,
        IShipmentRequestItemReposatory reqrepo,
        ICustomerReposatory cutrepo,
        ICurrentUserService servicerepo,
        IMapper mapper)
        : IRequestHandler<
            CreateShipmentRequestsCommand,
            ShipmentRequestDTO>
    {
        public async Task<ShipmentRequestDTO> Handle(
            CreateShipmentRequestsCommand request,
            CancellationToken cancellationToken)
        {
            // =====================================================
            // Get Logged-in User Id
            // =====================================================

            var userId = servicerepo.UserId;


            // =====================================================
            // Get Customer linked to current User
            // =====================================================

            var customer =
                await cutrepo.GetByCustomerId(userId);

            if (customer == null)
            {
                throw new UnauthorizedAccessException(
                    "Customer profile not found.");
            }


            // =====================================================
            // Create Shipment Request
            // =====================================================

            var shipmentRequest = new ShipmentRequest
            {
                CustomerId = customer.Id,

                PickupWarehouseId =
                    request.PickupWarehouseId,

                DestinationAddress =
                    request.DestinationAddress,

                Status =
                    ShipmentRequestStatus.Pending
            };


            await reqrepo.Add(shipmentRequest);

            await reqrepo.Save();


            // =====================================================
            // Create Shipment Items
            // =====================================================

            foreach (var item in request.Items)
            {
                var shipmentItem = new ShipmentItem
                {
                    ItemName = item.ItemName,
                    Quantity = item.Quantity,
                    Weight = item.Weight,
                    Description = item.Description,

                    ShipmentRequestId =
                        shipmentRequest.Id
                };

                await itemrepo.Add(shipmentItem);
            }

            await itemrepo.Save();


            // =====================================================
            // Get Full Request With Details
            // =====================================================

            var result =
                await reqrepo.GetByIdWithDetails(
                    shipmentRequest.Id);

            if (result == null)
            {
                throw new KeyNotFoundException(
                    "Shipment Request could not be retrieved after creation.");
            }


            return mapper.Map<ShipmentRequestDTO>(result);
        }
    }
}