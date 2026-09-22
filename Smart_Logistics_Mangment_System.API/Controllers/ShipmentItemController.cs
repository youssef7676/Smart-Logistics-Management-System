using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.CreateShipmentItem;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.DeleteShipmentItem;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.UpdateShipmentItem;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.Queries.GetAllShipmentItem;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.Queries.GetAllShipmentItemById;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentItemController(
        IMediator mediat,
        IMapper mapper) : ControllerBase
    {
        // ============================================
        // Get All Shipment Items
        // Admin + Employee
        // ============================================

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAllShipmentItems()
        {
            GetAllShipmentItemQueries queries =
                new GetAllShipmentItemQueries();

            List<ShipmentItemDTO> response =
                await mediat.Send(queries);

            return Ok(response);
        }


        // ============================================
        // Get Shipment Item By Id
        // Admin + Employee
        // ============================================

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetShipmentItemsById(int id)
        {
            GetAllShipmentItemByIdQueries queries =
                new GetAllShipmentItemByIdQueries(id);

            var response =
                await mediat.Send(queries);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "ShipmentItem not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Add New Shipment Item
        // Admin + Employee
        // ============================================

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AddNewShipmentItem(
            CreateShipmentItemCommand obj)
        {
            try
            {
                var response =
                    await mediat.Send(obj);

                return Ok(new
                {
                    message = "New ShipmentItem Added successfully",
                    ShipmentItemId = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to Add New ShipmentItem",
                    detail =
                        ex.InnerException?.InnerException?.Message
                        ?? ex.InnerException?.Message
                        ?? ex.Message
                });
            }
        }


        // ============================================
        // Update Shipment Item
        // Admin + Employee
        // ============================================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateShipmentItem(
            int id,
            UpdateShipmentItemCommand obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot modify This ShipmentItem."
                });
            }

            var response =
                await mediat.Send(obj);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "ShipmentItem not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Delete Shipment Item
        // Admin Only
        // ============================================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteShipmentItems(
            int id,
            IShipmentItemReposatory itemrepo,
            DeleteShipmentItemCommand obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot Delete This ShipmentItem."
                });
            }

            var ware =
                await itemrepo.GetById(id);

            if (ware == null)
            {
                return NotFound(new
                {
                    message = "ShipmentItem not found"
                });
            }

            await itemrepo.Delete(ware.Id);
            await itemrepo.Save();

            return Ok(new
            {
                message = "ShipmentItem deleted successfully"
            });
        }
    }
}
