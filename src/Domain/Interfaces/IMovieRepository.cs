using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll(bool includeDeleted = false);
        Movie? GetById(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(int id); // baja lógica
    }
}
