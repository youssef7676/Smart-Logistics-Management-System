using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Logistics_Mangment_System.Application.Vehicles.Commands.CreateVehicleCommand;
using Smart_Logistics_Mangment_System.Application.Vehicles.Commands.DeleteVehicleCommand;
using Smart_Logistics_Mangment_System.Application.Vehicles.Commands.UpdateVehicleCommand;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using Smart_Logistics_Mangment_System.Application.Vehicles.Queries.GetAllVehicle;
using Smart_Logistics_Mangment_System.Application.Vehicles.Queries.GetAllVehicleById;
using Smart_Logistics_Mangment_System.Domain.Interfaces;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController(
        IMediator mediat,
        IMapper mapper) : ControllerBase
    {
        // ============================================
        // Get All Vehicles
        // Admin + Employee
        // ============================================

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAllVehicles()
        {
            GetAllVehicleQueries queries =
                new GetAllVehicleQueries();

            List<VehicleDTO> response =
                await mediat.Send(queries);

            return Ok(response);
        }


        // ============================================
        // Get Vehicle By Id
        // Admin + Employee
        // ============================================

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            GetAllVehiclesByIdQueries queries =
                new GetAllVehiclesByIdQueries(id);

            var response =
                await mediat.Send(queries);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "Vehicle not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Add New Vehicle
        // Admin + Employee
        // ============================================

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AddNewVehicle(
            CreateVehiclesCommands obj)
        {
            try
            {
                var response =
                    await mediat.Send(obj);

                return Ok(new
                {
                    message = "New Vehicle Added successfully",
                    VehicleId = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to Add New Vehicle",
                    detail = ex.InnerException?.InnerException?.Message
                             ?? ex.InnerException?.Message
                             ?? ex.Message
                });
            }
        }


        // ============================================
        // Update Vehicle
        // Admin + Employee
        // ============================================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateVehicle(
            int id,
            UpdateVehiclesCommand obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot modify This Vehicle."
                });
            }

            var response =
                await mediat.Send(obj);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "Vehicle not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Delete Vehicle
        // Admin Only
        // ============================================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVehicle(
            int id,
            IVehicleReposatory verpo,
            DeleteVehicleCommand obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot Delete This Vehicle."
                });
            }

            var ware =
                await verpo.GetById(id);

            if (ware == null)
            {
                return NotFound(new
                {
                    message = "Vehicle not found"
                });
            }

            await verpo.Delete(ware.Id);
            await verpo.Save();

            return Ok(new
            {
                message = "Vehicle deleted successfully"
            });
        }
    }
}
