using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Logistics_Mangment_System.Application.Customers.Commands.DeleteCustomerCommand;
using Smart_Logistics_Mangment_System.Application.Customers.Commands.UpdateCustomerCommand;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using Smart_Logistics_Mangment_System.Application.Customers.Queries.GetAllCustomerQueries;
using Smart_Logistics_Mangment_System.Application.Customers.Queries.GetAllCustomerQueriesById;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(
        IMediator mediat,
        IMapper mapper) : ControllerBase
    {
        // =========================
        // GET ALL CUSTOMERS
        // =========================

        [HttpGet]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                GetAllCustomerQueries queries = new();

                var response = await mediat.Send(queries);

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
        // GET CUSTOMER BY ID
        // =========================

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                GetAllCustomerQueriesById queries = new(id);

                var response = await mediat.Send(queries);

                if (response == null)
                    return NotFound(new
                    {
                        message = "Customer not found"
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
        // UPDATE CUSTOMER
        // =========================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> UpdateCustomer(
    int id,
    UpdateCustomersCommand obj)
        {
            try
            {
                if (id != obj.Id)
                {
                    return BadRequest(new
                    {
                        message = "You cannot modify this Customer."
                    });
                }

                var response = await mediat.Send(obj);

                if (response == null)
                    return NotFound(new
                    {
                        message = "Customer not found"
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
        // DELETE CUSTOMER
        // =========================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomer(
      int id,
      DeleteCustomersCommand obj)
        {
            if (id != obj.Id)
            {
                return BadRequest(new
                {
                    message = "You cannot delete this Customer."
                });
            }

            var response = await mediat.Send(obj);

            if (response == null)
                return NotFound(new
                {
                    message = "Customer not found"
                });

            return Ok(new
            {
                message = "Customer deleted successfully",
                customer = response
            }); 
        }
    }
}

