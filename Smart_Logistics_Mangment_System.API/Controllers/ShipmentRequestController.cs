using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.ApproveShipmentRequest;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.CancelShipmentRequest;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.DeleteShipmentRequest;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.RejectShipmentRequest;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.UpdateShipmentRequest;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Queries.GetAllShipmentRequest;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Queries.GetAllShipmentRequestById;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentRequestController(IMediator mediat) : ControllerBase
    {

        // =========================================================
        // Get All Shipment Requests
        // Admin + Employee + Customer
        //
        // Admin/Employee => All Requests
        // Customer       => Own Requests Only
        // =========================================================

        [HttpGet]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetAllRequests(
            [FromQuery] GetAllShipmentRequestQueries queries)
        {
            var response = await mediat.Send(queries);

            return Ok(response);
        }


        // =========================================================
        // Get Shipment Request By Id
        // Admin + Employee + Customer
        //
        // Admin/Employee => Any Request
        // Customer       => Own Request Only
        // =========================================================

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetRequestsById(int id)
        {
            try
            {
                var queries = new GetAllShipmentRequestByIdQueries(id);

                var response = await mediat.Send(queries);

                if (response == null)
                {
                    return NotFound(new
                    {
                        message = "ShipmentRequest not found"
                    });
                }

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    message = ex.Message
                });
            }
        }


        // =========================================================
        // Create Shipment Request
        // Customer Only
        // =========================================================

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateShipmentRequest(
            [FromBody] CreateShipmentRequestsCommand command)
        {
            var result =
                await mediat.Send(command);

            return Ok(result);
        }


        // =========================================================
        // Update Shipment Request
        // Admin + Employee + Customer
        //
        // Admin/Employee => Can update any request
        // Customer       => Can update own request only
        // =========================================================

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> UpdateRequest(
    int id,
    UpdateShipmentRequestCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot modify this Request."
                });
            }

            try
            {
                var response = await mediat.Send(command);

                if (response == null)
                {
                    return NotFound(new
                    {
                        message = "ShipmentRequest not found."
                    });
                }

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new
                    {
                        message = ex.Message
                    });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        // =========================================================
        // Approve Shipment Request
        // Admin + Employee Only
        // =========================================================

        [HttpPut("{id:int}/approve")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> ApproveShipmentRequest(int id)
        {
            try
            {
                var command =
                    new ApproveShipmentRequestCommand(id);

                var response =
                    await mediat.Send(command);

                if (response == null)
                {
                    return NotFound(new
                    {
                        message = "ShipmentRequest not found."
                    });
                }

                return Ok(new
                {
                    message = "ShipmentRequest approved successfully.",
                    shipmentRequest = response
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new
                    {
                        message = ex.Message
                    });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        [HttpPut("{id:int}/reject")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> RejectShipmentRequest(int id)
        {
            try
            {
                var command =
                    new RejectShipmentRequestCommand(id);

                var response =
                    await mediat.Send(command);

                if (response == null)
                {
                    return NotFound(new
                    {
                        message = "ShipmentRequest not found."
                    });
                }

                return Ok(new
                {
                    message = "ShipmentRequest rejected successfully.",
                    shipmentRequest = response
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new
                    {
                        message = ex.Message
                    });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        [HttpPut("{id:int}/cancel")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CancelShipmentRequest(int id)
        {
            try
            {
                var command =
                    new CancelShipmentRequestCommand(id);

                var response =
                    await mediat.Send(command);

                if (response == null)
                {
                    return NotFound(new
                    {
                        message = "ShipmentRequest not found."
                    });
                }

                return Ok(new
                {
                    message = "ShipmentRequest cancelled successfully.",
                    shipmentRequest = response
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new
                    {
                        message = ex.Message
                    });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        // =========================================================
        // Delete Shipment Request
        // Admin Only
        // =========================================================

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRequests(
            int id,
            [FromBody] DeleteShipmentRequestCommand command)
        {
            // Prevent URL ID != Body ID
            if (id != command.Id)
            {
                return BadRequest(new
                {
                    message =
                        "The ShipmentRequest ID in the URL must match the ID in the request body."
                });
            }

            var response =
                await mediat.Send(command);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "ShipmentRequest not found."
                });
            }

            return Ok(new
            {
                message =
                    "ShipmentRequest deleted successfully.",

                shipmentRequest = response
            });
        }
    }
}
