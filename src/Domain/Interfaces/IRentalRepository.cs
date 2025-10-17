using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental> CreateAsync(Rental rental);
        Task<Movie?> GetMovieByIdAsync(int movieId);
        Task<User?> GetClienteByIdAsync(int clienteId);
        Task UpdateMovieAsync(Movie movie);
        Task<List<Rental>> GetOrdersByMovieIdAsync(int movieId);
    }
}
