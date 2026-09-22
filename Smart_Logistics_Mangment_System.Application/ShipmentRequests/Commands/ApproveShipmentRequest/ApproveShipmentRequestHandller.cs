using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.ApproveShipmentRequest
{
    public class ApproveShipmentRequestHandler(IShipmentRequestItemReposatory reqrepo,
        ICurrentUserService currentUserService,IMapper mapper)
        : IRequestHandler<ApproveShipmentRequestCommand, ShipmentRequestDTO>
    {
        public async Task<ShipmentRequestDTO> Handle(ApproveShipmentRequestCommand request,CancellationToken cancellationToken)
        {
            // ============================================
            // Authorization
            // Admin + Employee only
            // ============================================

            var role = currentUserService.Role;

            if (role != "Admin" && role != "Employee")
            {
                throw new UnauthorizedAccessException(
                    "Only Admin or Employee can approve shipment requests.");
            }


            // ============================================
            // Get Shipment Request
            // ============================================

            var shipmentRequest =
                await reqrepo.GetByIdWithDetails(request.Id);

            if (shipmentRequest == null)
            {
                return null;
            }


            // ============================================
            // Check Current Status
            // ============================================

            if (shipmentRequest.Status != ShipmentRequestStatus.Pending)
            {
                throw new InvalidOperationException(
                    "Only pending shipment requests can be approved.");
            }


            // ============================================
            // Approve
            // ============================================

            shipmentRequest.Status =
                ShipmentRequestStatus.Approved;


            reqrepo.Update(shipmentRequest);

            await reqrepo.Save();


            // ============================================
            // Return Updated Request
            // ============================================

            return mapper.Map<ShipmentRequestDTO>(
                shipmentRequest);
        }
    }
}