using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.RejectShipmentRequest
{
    public class RejectShipmentRequestHandler(IShipmentRequestItemReposatory reqrepo,
        ICurrentUserService currentUserService,IMapper mapper)
        : IRequestHandler<RejectShipmentRequestCommand,ShipmentRequestDTO>
    {
        public async Task<ShipmentRequestDTO> Handle(
            RejectShipmentRequestCommand request,
            CancellationToken cancellationToken)
        {
            var role = currentUserService.Role;

            // Only Admin and Employee can reject
            if (role != "Admin" && role != "Employee")
            {
                throw new UnauthorizedAccessException(
                    "Only Admin or Employee can reject shipment requests.");
            }

            var shipmentRequest =
                await reqrepo.GetByIdWithDetails(request.Id);

            if (shipmentRequest == null)
            {
                return null;
            }

            // Only Pending requests can be rejected
            if (shipmentRequest.Status != ShipmentRequestStatus.Pending)
            {
                throw new InvalidOperationException(
                    "Only pending shipment requests can be rejected.");
            }

            shipmentRequest.Status =
                ShipmentRequestStatus.Rejected;

            reqrepo.Update(shipmentRequest);

            await reqrepo.Save();

            return mapper.Map<ShipmentRequestDTO>(
                shipmentRequest);
        }
    }
}