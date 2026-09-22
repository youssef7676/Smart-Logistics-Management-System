using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.CancelShipmentRequest
{
    public class CancelShipmentRequestHandler(IShipmentRequestItemReposatory reqrepo,
        ICurrentUserService currentUserService,IMapper mapper)
        : IRequestHandler<CancelShipmentRequestCommand,ShipmentRequestDTO>
    {
        public async Task<ShipmentRequestDTO> Handle(
            CancelShipmentRequestCommand request,
            CancellationToken cancellationToken)
        {
            var userId = currentUserService.UserId;
            var role = currentUserService.Role;

            // Only Customer can cancel their own request
            if (role != "Customer")
            {
                throw new UnauthorizedAccessException(
                    "Only Customer can cancel shipment requests.");
            }

            var shipmentRequest =
                await reqrepo.GetByIdWithDetails(request.Id);

            if (shipmentRequest == null)
            {
                return null;
            }

            // Make sure the request belongs to the logged-in customer
            if (shipmentRequest.Customer == null)
            {
                throw new KeyNotFoundException(
                    "Customer not found.");
            }

            if (shipmentRequest.Customer.UserId != userId)
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to cancel this shipment request.");
            }

            // Only Pending requests can be cancelled
            if (shipmentRequest.Status != ShipmentRequestStatus.Pending)
            {
                throw new InvalidOperationException(
                    "Only pending shipment requests can be cancelled.");
            }

            shipmentRequest.Status =
                ShipmentRequestStatus.Cancelled;

            reqrepo.Update(shipmentRequest);

            await reqrepo.Save();

            return mapper.Map<ShipmentRequestDTO>(
                shipmentRequest);
        }
    }
}