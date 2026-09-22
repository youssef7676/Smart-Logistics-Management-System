using AutoMapper;
using MediatR;

using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Queries.GetAllShipmentRequest
{
    public class GetAllShipmentRequestHandller(
        IShipmentRequestItemReposatory reqrepo,
        ICustomerReposatory customerRepo,
        ICurrentUserService currentUserService,
        IMapper mapper)
        : IRequestHandler<
            GetAllShipmentRequestQueries,
            List<ShipmentRequestDTO>>
    {
        public async Task<List<ShipmentRequestDTO>> Handle(
            GetAllShipmentRequestQueries request,
            CancellationToken cancellationToken)
        {
            var userId =
                currentUserService.UserId;

            var role =
                currentUserService.Role;


            // =====================================================
            // Admin + Employee
            // Can see ALL Shipment Requests
            // =====================================================

            if (role == "Admin" || role == "Employee")
            {
                var requests =
                    await reqrepo.GetAllWithDetails(
                        request.PageNumber,
                        request.PageSize);

                return mapper.Map<List<ShipmentRequestDTO>>(
                    requests);
            }


            // =====================================================
            // Customer
            // Can see OWN Shipment Requests ONLY
            // =====================================================

            if (role == "Customer")
            {
                var customer =
                    await customerRepo.GetByCustomerId(userId);

                if (customer == null)
                {
                    throw new UnauthorizedAccessException(
                        "Customer profile not found.");
                }

                var requests =
                    await reqrepo.GetAllByCustomerIdWithDetails(
                        customer.Id,
                        request.PageNumber,
                        request.PageSize);

                return mapper.Map<List<ShipmentRequestDTO>>(
                    requests);
            }


            // =====================================================
            // Any Other Role
            // =====================================================

            throw new UnauthorizedAccessException(
                "You are not authorized to access shipment requests.");
        }
    }
}