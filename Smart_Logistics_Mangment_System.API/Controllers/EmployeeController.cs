using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Smart_Logistics_Mangment_System.Application.Employees.Commands.CreateEmployeeCommand;
using Smart_Logistics_Mangment_System.Application.Employees.Commands.DeleteEmployeeCommand;
using Smart_Logistics_Mangment_System.Application.Employees.Commands.UpdateEmployeeCommand;
using Smart_Logistics_Mangment_System.Application.Employees.DTOs;
using Smart_Logistics_Mangment_System.Application.Employees.Queries.GetAllEmployee;
using Smart_Logistics_Mangment_System.Application.Employees.Queries.GetAllEmployeeById;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeeController(
        IMediator mediat) : ControllerBase
    {
        // ============================================
        // Get All Employees
        // Admin Only
        // ============================================

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            GetAllEmployeesQueries queries =
                new();

            var response =
                await mediat.Send(queries);

            return Ok(response);
        }


        // ============================================
        // Get Employee By Id
        // Admin Only
        // ============================================

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            GetAllEmployeeQueriesById queries =
                new(id);

            var response =
                await mediat.Send(queries);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "Employee not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Add New Employee
        // Admin Only
        // ============================================

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee(
            CreateEmployeesCommand command)
        {
            try
            {
                var response =
                    await mediat.Send(command);

                return Ok(new
                {
                    message = "New Employee added successfully",
                    employeeId = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to add new Employee",
                    detail =
                        ex.InnerException?.InnerException?.Message
                        ?? ex.InnerException?.Message
                        ?? ex.Message
                });
            }
        }


        // ============================================
        // Update Employee
        // Admin Only
        // ============================================

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            UpdateEmployeesCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot modify this Employee."
                });
            }

            var response =
                await mediat.Send(command);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "Employee not found"
                });
            }

            return Ok(response);
        }


        // ============================================
        // Delete Employee
        // Admin Only
        // ============================================

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(
            int id,
            DeleteEmployeesCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot delete this Employee."
                });
            }

            var response =
                await mediat.Send(command);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "Employee not found"
                });
            }

            return Ok(new
            {
                message = "Employee deleted successfully",
                employee = response
            });
        }
    }
}