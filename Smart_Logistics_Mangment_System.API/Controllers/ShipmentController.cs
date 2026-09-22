using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Logistics_Mangment_System.Application.Shipments.Commands.AssignShipment;
using Smart_Logistics_Mangment_System.Application.Shipments.Commands.CreateShipment;
using Smart_Logistics_Mangment_System.Application.Shipments.Commands.DeleteShipment;
using Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipment;
using Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipmentLocation;
using Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipmentStatus;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Application.Shipments.Queries.GetAllShipments;
using Smart_Logistics_Mangment_System.Application.Shipments.Queries.GetAllShipmentsById;
using Smart_Logistics_Mangment_System.Application.Shipments.Queries.GetShipmentETA;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController(
        IMediator mediat,
        IMapper mapper) : ControllerBase
    {
        // ============================================
        // Get All Shipments
        // Admin + Employee
        // ============================================

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAllShipments()
        {
            GetAllShipmentQueries queries =
                new GetAllShipmentQueries();

            List<ShipmentDTO> response =
                await mediat.Send(queries);

            return Ok(response);
        }


        // ============================================
        // Get Shipment By Id
        // Admin + Employee
        // ============================================

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetShipmentsById(int id)
        {
            GetAllShipmentByIdQueries queries =
                new GetAllShipmentByIdQueries(id);

            var response =
                await mediat.Send(queries);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "Shipment not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Add New Shipment
        // Admin + Employee
        // ============================================

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AddNewShipment(
            CreateShipmentCommand obj)
        {
            try
            {
                var response =
                    await mediat.Send(obj);

                return Ok(new
                {
                    message = "New Shipment Added successfully",
                    ShipmentId = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to Add New Shipment",
                    detail =
                        ex.InnerException?.InnerException?.Message
                        ?? ex.InnerException?.Message
                        ?? ex.Message
                });
            }
        }


        // ============================================
        // Update Shipment
        // Admin + Employee
        // ============================================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateShipment(
            int id,
            UpdateShipmentCommand obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot modify This Shipment."
                });
            }

            var response =
                await mediat.Send(obj);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "Shipment not found"
                });
            }

            return Ok(response);
        }


        [HttpPut("Update-Status")]
        [Authorize(Roles = "Admin,Employee,Driver")]
        public async Task<IActionResult> UpdateStatus(
     UpdateShipmentStatusCommand command)
        {
            try
            {
                var response = await mediat.Send(command);

                return Ok(new
                {
                    message = "Shipment status updated successfully.",
                    shipmentTracking = response
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new
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


        // ============================================
        // Assign Driver + Vehicle
        // Admin + Employee
        // ============================================

        [HttpPut("{id}/assign")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AssignShipment(
            int id,
            AssignShipmentCommand obj)
        {
            if (id != obj.ShipmentId)
            {
                return BadRequest(new
                {
                    message = "Shipment Id does not match."
                });
            }

            try
            {
                var response = await mediat.Send(obj);

                return Ok(new
                {
                    message = "Driver and Vehicle assigned successfully",
                    shipment = response
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
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


        [HttpGet("{id}/ETA")]
        [Authorize]
        public async Task<IActionResult> GetETA(int id)
        {
            try
            {
                var response =
                    await mediat.Send(
                        new GetShipmentETAQuery
                        {
                            ShipmentId = id
                        });

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new
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


        [HttpPut("Update-Location")]
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> UpdateLocation(UpdateShipmentLocationCommand command)
        {
            try
            {
                var response =
                    await mediat.Send(command);

                return Ok(new
                {
                    message =
                        "Shipment location updated successfully.",

                    shipment = response
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new
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
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        // ============================================
        // Delete Shipment
        // Admin Only
        // ============================================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteShipments(
            int id,
            IShipmentReposatory shiprepo,
            DeleteShipmentCommand obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot Delete This Shipment."
                });
            }

            var ware =
                await shiprepo.GetById(id);

            if (ware == null)
            {
                return NotFound(new
                {
                    message = "Shipment not found"
                });
            }

            await shiprepo.Delete(ware.Id);
            await shiprepo.Save();

            return Ok(new
            {
                message = "Shipment deleted successfully"
            });
        }
    }
}
