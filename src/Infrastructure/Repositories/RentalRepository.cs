using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationContext _context;

        public RentalRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Rental> CreateAsync(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<Movie?> GetMovieByIdAsync(int movieId)
        {
            return await _context.Movies
            
                .Include(b => b.Category)
            
                .FirstOrDefaultAsync(b => b.Id == movieId && !b.IsDeleted);
        }

        public async Task<User?> GetClienteByIdAsync(int clienteId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == clienteId && !u.IsDeleted);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Rental>> GetOrdersByMovieIdAsync(int movieId)
        {
            return await _context.Rentals
                .Include(o => o.Client)
                .Where(o => o.MovieId == movieId)
                .Include(o => o.Movie)
                .ToListAsync();
        }

       
    }
}

