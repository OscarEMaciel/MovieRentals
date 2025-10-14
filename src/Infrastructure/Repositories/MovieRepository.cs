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
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationContext _context;

        public MovieRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> GetAll(bool includeDeleted = false)
        {
            var query = _context.Movies
             
                .Include(b => b.Category)
                .AsQueryable();

            if (!includeDeleted)
                query = query.Where(b => !b.IsDeleted);

            return query.ToList();
        }

        public Movie? GetById(int id)
        {
            return _context.Movies
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Add(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var existingMovie = _context.Movies.Find(id);
            if (existingMovie != null)
            {
                existingMovie.IsDeleted = true; // baja lógica
                _context.SaveChanges();
            }
        }
    }
}
