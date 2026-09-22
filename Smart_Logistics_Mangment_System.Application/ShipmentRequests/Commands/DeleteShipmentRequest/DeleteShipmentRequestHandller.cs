using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System.Security.Claims;

using AutoMapper;
using MediatR;

using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.DeleteShipmentRequest
{
    public class DeleteShipmentRequestHandller(
        IShipmentRequestItemReposatory reqrepo,
        IMapper mapper,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            DeleteShipmentRequestCommand,
            ShipmentRequestDTO>
    {
        public async Task<ShipmentRequestDTO> Handle(
            DeleteShipmentRequestCommand request,
            CancellationToken cancellationToken)
        {
            // =====================================================
            // Role Check
            // =====================================================

            if (currentUserService.Role != "Admin")
            {
                throw new UnauthorizedAccessException(
                    "Only Admin can delete shipment requests.");
            }


            // =====================================================
            // Get Shipment Request
            // =====================================================

            var shipmentRequest =
                await reqrepo.GetByIdWithDetails(
                    request.Id);

            if (shipmentRequest == null)
            {
                throw new KeyNotFoundException(
                    "Shipment Request not found.");
            }


            // =====================================================
            // Don't Delete After Shipment Creation
            // =====================================================

            if (shipmentRequest.ShipmentId.HasValue)
            {
                throw new InvalidOperationException(
                    "Cannot delete a shipment request after shipment creation.");
            }


            // =====================================================
            // Delete
            // =====================================================

            reqrepo.Delete(
                shipmentRequest.Id);

            await reqrepo.Save();


            // =====================================================
            // Return Deleted Request
            // =====================================================

            return mapper.Map<ShipmentRequestDTO>(
                shipmentRequest);
        }
    }
}