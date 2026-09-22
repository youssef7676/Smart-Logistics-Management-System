using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.CreateShipmentTracking;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.DeleteShipmentTracking;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.UpdateShipmentTracking;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Queries.GetAllShipmentTracking;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Queries.GetAllShipmentTrackingById;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentTrackingController(
        IMediator mediat,
        IMapper mapper) : ControllerBase
    {
        // ============================================
        // Get All Shipment Tracking
        // Admin + Employee + Driver
        // ============================================

        [HttpGet]
        [Authorize(Roles = "Admin,Employee,Driver")]
        public async Task<IActionResult> GetAllTracking()
        {
            GetAllShipmentTrackingQueries queries =
                new GetAllShipmentTrackingQueries();

            List<ShipmentTrackingDTO> response =
                await mediat.Send(queries);

            return Ok(response);
        }


        // ============================================
        // Get Tracking By Id
        // Admin + Employee + Driver
        // ============================================

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee,Driver")]
        public async Task<IActionResult> GetTrackingById(int id)
        {
            GetAllShipmentTrackingByIdQueries queries =
                new GetAllShipmentTrackingByIdQueries(id);

            var response =
                await mediat.Send(queries);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "ShipmentTracking not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Add New Shipment Tracking
        // Admin + Employee + Driver
        // ============================================

        //[HttpPost]
        //[Authorize(Roles = "Admin,Employee,Driver")]
        //public async Task<IActionResult> AddNewShipmentTracking(
        //    CreateShipmentTrackingCommand obj)
        //{
        //    try
        //    {
        //        var response =
        //            await mediat.Send(obj);

        //        return Ok(new
        //        {
        //            message = "New ShipmentTracking Added successfully",
        //            ShipmentTrackingId = response
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new
        //        {
        //            message = "Failed to Add New ShipmentTracking",
        //            detail =
        //                ex.InnerException?.InnerException?.Message
        //                ?? ex.InnerException?.Message
        //                ?? ex.Message
        //        });
        //    }
        //}


        // ============================================
        // Update Shipment Tracking
        // Admin + Employee + Driver
        // ============================================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee,Driver")]
        public async Task<IActionResult> UpdateTracking(
            int id,
            UpdateShipmentTrackingCommand obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot modify This Tracking."
                });
            }

            var response =
                await mediat.Send(obj);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "ShipmentTracking not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Delete Shipment Tracking
        // Admin Only
        // ============================================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTrackings(
            int id,
            IShipmentTrackingReposatory trackrepo,
            DeleteShipmentTrackingCommand obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot Delete This Tracking."
                });
            }

            var ware =
                await trackrepo.GetById(id);

            if (ware == null)
            {
                return NotFound(new
                {
                    message = "ShipmentTracking not found"
                });
            }

            await trackrepo.Delete(ware.Id);
            await trackrepo.Save();

            return Ok(new
            {
                message = "ShipmentTracking deleted successfully"
            });
        }
    }
}
