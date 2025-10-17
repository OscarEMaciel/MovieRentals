using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Models;
using Aplication.Responses;
using Domain.Entities;
using Domain.Interfaces;

namespace Aplication.Services
{
    public class RentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<RentalReponse> CreateOrderAsync(int clienteId, int movieId, int cantidad)
        {
            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0.");

            var movie = await _rentalRepository.GetMovieByIdAsync(movieId);
            var cliente = await _rentalRepository.GetClienteByIdAsync(clienteId);

            if (movie == null)
                throw new Exception("La pelicula no existe o fue eliminada."); // <-- aclaramos el error
            if (cliente == null)
                throw new Exception("El cliente no existe.");

            if (movie.Stock < cantidad)
                throw new Exception("No hay suficiente stock para esta pelicula.");

            // Descontar stock
            movie.Stock -= cantidad;
            await _rentalRepository.UpdateMovieAsync(movie);

            var order = new Rental
            {
                ClientId = clienteId,
                MovieId = movieId,
                Cantidad = cantidad,
                Fecha = DateTime.UtcNow
            };

            var savedOrder = await _rentalRepository.CreateAsync(order);

            return new RentalReponse
            {
                ClienteNombre = cliente.Name,
                PeliculaTitulo = movie.Title,
                CategoriaNombre = movie.Category?.Name,
                Cantidad = savedOrder.Cantidad,
                Fecha = savedOrder.Fecha,
                PrecioTotal = savedOrder.Cantidad * movie.Price
            };
        }


        // GET con lista de clientes
        public async Task<List<RentalDto>> GetClientsByMovieIdAsync(int movieId)
        {
            var orders = await _rentalRepository.GetOrdersByMovieIdAsync(movieId);

            return orders.Select(o => new RentalDto
            {
                ClienteId = o.ClientId,
                ClienteNombre = o.Client?.Name,
                Cantidad = o.Cantidad,
                PrecioTotal = o.Cantidad * (o.Movie?.Price ?? 0),
                Fecha = o.Fecha
            }).ToList();
        }
    }
}

