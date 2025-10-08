using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Rental
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }   // puede ser null, crear un Order solo con MovieId y ClientId, y EF se encarga de linkear las relaciones.

        public int ClientId { get; set; }
        public User? Client { get; set; } // puede ser null

        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
