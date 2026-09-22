using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Queries.GetAllShipmentRequestById;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

using AutoMapper;
using MediatR;

using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Queries.GetAllShipmentRequestById
{
    public class GetAllShipmentRequestByIdHandller(
        IShipmentRequestItemReposatory reqrepo,
        ICustomerReposatory customerRepo,
        ICurrentUserService currentUserService,
        IMapper mapper)
        : IRequestHandler<
            GetAllShipmentRequestByIdQueries,
            ShipmentRequestDTO>
    {
        public async Task<ShipmentRequestDTO> Handle(
            GetAllShipmentRequestByIdQueries request,
            CancellationToken cancellationToken)
        {
            var userId =
                currentUserService.UserId;

            var role =
                currentUserService.Role;


            // =====================================================
            // Get Shipment Request
            // =====================================================

            var shipmentRequest =
                await reqrepo.GetByIdWithDetails(
                    request.Id);

            if (shipmentRequest == null)
            {
                return null;
            }


            // =====================================================
            // Admin + Employee
            // Can access ANY request
            // =====================================================

            if (role == "Admin" || role == "Employee")
            {
                return mapper.Map<ShipmentRequestDTO>(
                    shipmentRequest);
            }


            // =====================================================
            // Customer
            // Can access OWN request ONLY
            // =====================================================

            if (role == "Customer")
            {
                var customer =
                    await customerRepo.GetByCustomerId(
                        userId);

                if (customer == null)
                {
                    throw new UnauthorizedAccessException(
                        "Customer profile not found.");
                }

                if (shipmentRequest.CustomerId != customer.Id)
                {
                    throw new UnauthorizedAccessException(
                        "You are not allowed to access this shipment request.");
                }

                return mapper.Map<ShipmentRequestDTO>(
                    shipmentRequest);
            }


            throw new UnauthorizedAccessException(
                "You are not authorized to access this shipment request.");
        }
    }
}