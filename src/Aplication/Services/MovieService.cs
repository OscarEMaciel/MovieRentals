using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Models;
using Aplication.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;

namespace Aplication.Services
{
    public class MovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IEnumerable<Movie> GetAll(bool includeDeleted = false)
        {
            return _movieRepository.GetAll(includeDeleted);
        }

        public Movie? GetById(int id)
        {
            return _movieRepository.GetById(id); // Tu repo ya hace Include(...)
        }

        // Crear pelicula y devolver DTO consistente
        public MovieDto? CreateMovie(CreateMovieRequest request)
        {
            var Movie = new Movie
            {
                Title = request.Title,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                IsDeleted = false
            };

            _movieRepository.Add(Movie);

            // Volver a cargar con relaciones (tu GetById ya incluye/Category)

            var createdMovie = _movieRepository.GetById(Movie.Id);
            if (createdMovie == null) return null;

            // Mapear a DTO
            return new MovieDto
            {
                Id = createdMovie.Id,
                Title = createdMovie.Title,
                Price = createdMovie.Price,
                Stock = createdMovie.Stock,
                CategoryName = createdMovie.Category?.Name
            };
        }

        public bool Update(int id, UpdateMovieRequest request)
        {
            var movie = _movieRepository.GetById(id);

            if (movie == null || movie.IsDeleted)
                return false;

            movie.Title = request.Title;
            movie.Price = request.Price;
            movie.Stock = request.Stock;
            movie.CategoryId = request.CategoryId;

            _movieRepository.Update(movie);

            return true;

        }

        public bool Delete(int id)
        {
            var movie = _movieRepository.GetById(id);

            if (movie == null || movie.IsDeleted)
                return false;

            _movieRepository.Delete(id); // baja lógica

            return true;
        }
    }
}
