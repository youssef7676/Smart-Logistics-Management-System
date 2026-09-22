using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Smart_Logistics_Mangment_System.Application.Drivers.Commands.CreateDriversCommand;
using Smart_Logistics_Mangment_System.Application.Drivers.Commands.DeleteDriversCommand;
using Smart_Logistics_Mangment_System.Application.Drivers.Commands.UpdateDriversCommand;
using Smart_Logistics_Mangment_System.Application.Drivers.DTOs;
using Smart_Logistics_Mangment_System.Application.Drivers.Queries.GetAllDrivers;
using Smart_Logistics_Mangment_System.Application.Drivers.Queries.GetAllDriversById;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController(
        IMediator mediat,
        IMapper mapper) : ControllerBase
    {
        // =========================
        // GET ALL
        // =========================

        [HttpGet]
        [Authorize(Roles = "Admin,Employee,Driver")]
        public async Task<IActionResult> GetAllDrivers()
        {
            try
            {
                GetAllDriversQueries queries = new();

                var response =
                    await mediat.Send(queries);

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new
                {
                    message = ex.Message
                });
            }
        }


        // =========================
        // GET BY ID
        // =========================

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee,Driver")]
        public async Task<IActionResult> GetDriverById(int id)
        {
            try
            {
                GetAllDriversQueriesById queries =
                    new(id);

                var response =
                    await mediat.Send(queries);

                if (response == null)
                    return NotFound(new
                    {
                        message = "Driver not found"
                    });

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new
                {
                    message = ex.Message
                });
            }
        }


        // =========================
        // CREATE
        // =========================

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> CreateDriver(
     CreateDriverCommand command)
        {
            var response = await mediat.Send(command);

            return Ok(new
            {
                message = "Driver created successfully",
                driverId = response
            });
        }


        // =========================
        // UPDATE
        // =========================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee,Driver")]
        public async Task<IActionResult> UpdateDriver(
            int id,
            UpdateDriverCommand command)
        {
            try
            {
                if (id != command.Id)
                {
                    return BadRequest(new
                    {
                        message = "You cannot modify this Driver."
                    });
                }

                var response =
                    await mediat.Send(command);

                if (response == null)
                    return NotFound(new
                    {
                        message = "Driver not found"
                    });

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new
                {
                    message = ex.Message
                });
            }
        }


        // =========================
        // DELETE
        // =========================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDriver(
            int id,
            DeleteDriverCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot delete this Driver."
                });
            }

            var response =
                await mediat.Send(command);

            if (response == null)
                return NotFound(new
                {
                    message = "Driver not found"
                });

            return Ok(new
            {
                message = "Driver deleted successfully",
                driver = response
            });
        }
    }
}