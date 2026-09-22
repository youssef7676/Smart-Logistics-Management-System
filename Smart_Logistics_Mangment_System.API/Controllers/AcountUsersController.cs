using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Smart_Logistics_Mangment_System.Application.AppUsers.Commands;
using Smart_Logistics_Mangment_System.Application.AppUsers.Commands.Register;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using Smart_Logistics_Mangment_System.Application.AppUsers.Queries.GetAllUers;
using Smart_Logistics_Mangment_System.Application.AppUsers.Queries.GetAllUersById;
using Smart_Logistics_Mangment_System.Application.RefreshTokenss.Commands;

namespace Smart_Logistics_Mangment_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountUsersController(
        IMediator mediat,
        IMapper mapper) : ControllerBase
    {
        // https://localhost:44393/signalr-test.html
        //    Admin
        //  "email": "admin@smartlogistics.test",
        //  "password": "Admin@123"
        //       
        // Driver
        //    "email": "sameh.abdelrahman57@smartlogistics.test",
        //    "password": "Passw0rd!"

        //  Employee
        //    "email":youssef.salem104@smartlogistics.test
        //    "password": "Passw0rd!"



        // ============================================
        // Register
        // Public
        // ============================================

        [HttpPost("Register-Customer")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await mediat.Send(command);

            return Ok(new
            {
                message = "Registration successful.",
                userId = result
            });
        }


        // ============================================
        // Login
        // Public
        // ============================================

        [HttpPost("User-login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            LoginCommand command)
        {
            var result = await mediat.Send(command);

            return Ok(result);
        }


        [HttpPost("Refresh-Token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(
    RefreshTokenCommand command)
        {
            try
            {
                var result = await mediat.Send(command);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    message = ex.Message
                });
            }
        }


        // ============================================
        // Get All Users
        // Admin Only
        // ============================================

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var queries = new GetAllUserQueries();

            var response = await mediat.Send(queries);

            return Ok(response);
        }


        // ============================================
        // Get User By Id
        // Admin Only
        // ============================================

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersById(int id)
        {
            var queries =
                new GetAllUserQueriesById(id);

            var response =
                await mediat.Send(queries);

            if (response == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            return Ok(response);
        }
    }
}








