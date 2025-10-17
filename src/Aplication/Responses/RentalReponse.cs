using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Responses
{
    public class RentalReponse
    {
        public string? ClienteNombre { get; set; }
        public string? PeliculaTitulo { get; set; }
        public string? CategoriaNombre { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}

