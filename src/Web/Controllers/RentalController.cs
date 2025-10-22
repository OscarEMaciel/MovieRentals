using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aplication.Services;
using Aplication.Models.Requests;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly RentalService _rentalService;

        public RentalController(RentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Cliente,Admin")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateRentalRequest request)
        {
            try
            {
                // Obtener el ID del usuario desde el token
                var clienteId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var order = await _rentalService.CreateOrderAsync(clienteId, request.MovieId, request.Cantidad);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("clients/{movieId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetClientsByMovieId(int movieId)
        {
            try
            {
                var clients = await _rentalService.GetClientsByMovieIdAsync(movieId);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
