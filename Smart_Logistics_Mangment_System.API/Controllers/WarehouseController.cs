using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Logistics_Mangment_System.Application.Warehouses.Commands.CreateWarehouseCommand;
using Smart_Logistics_Mangment_System.Application.Warehouses.Commands.DeleteWarehouseCommand;
using Smart_Logistics_Mangment_System.Application.Warehouses.Commands.UpdateWarehouseCommand;
using Smart_Logistics_Mangment_System.Application.Warehouses.Queries.GetAllWarehouse;
using Smart_Logistics_Mangment_System.Application.Warehouses.DTOs;
using Smart_Logistics_Mangment_System.Application.Warehouses.Queries.GetAllWarehouseById;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController(
        IMediator mediat,
        IMapper mapper) : ControllerBase
    {
        // ============================================
        // Get All Warehouses
        // Admin + Employee
        // ============================================

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAllwarehouse()
        {
            GetAllWareHouseQueries queries =
                new GetAllWareHouseQueries();

            List<DriversDTO> response =
                await mediat.Send(queries);

            return Ok(response);
        }


        // ============================================
        // Get Warehouse By Id
        // Admin + Employee
        // ============================================

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            GetWarehouseByIdQueries queries =
                new GetWarehouseByIdQueries(id);

            var response =
                await mediat.Send(queries);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "Warehouse not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Add New Warehouse
        // Admin + Employee
        // ============================================

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AddNewWarehouse(
            CreateDataWarehouseCommand obj)
        {
            try
            {
                var response =
                    await mediat.Send(obj);

                return Ok(new
                {
                    message = "New Warehouse Added successfully",
                    WarehouseId = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to Add New Warehouse",
                    detail =
                        ex.InnerException?.InnerException?.Message
                        ?? ex.InnerException?.Message
                        ?? ex.Message
                });
            }
        }


        // ============================================
        // Update Warehouse
        // Admin + Employee
        // ============================================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateWarehouse(
            int id,
            UpdateWarehouseCommands obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot modify This Warehouse."
                });
            }

            var response =
                await mediat.Send(obj);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "Warehouse not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Delete Warehouse
        // Admin Only
        // ============================================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteWarehouse(
            int id,
            IWarehouseReposatory warerepo,
            DeleteWarehouseCommands obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot Delete This Warehouse."
                });
            }

            var ware =
                await warerepo.GetById(id);

            if (ware == null)
            {
                return NotFound(new
                {
                    message = "Warehouse not found"
                });
            }

            await warerepo.Delete(ware.Id);
            await warerepo.Save();

            return Ok(new
            {
                message = "Warehouse deleted successfully"
            });
        }
    }
}

