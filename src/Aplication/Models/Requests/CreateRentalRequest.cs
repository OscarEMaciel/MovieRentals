using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Models.Requests
{
    public class CreateRentalRequest
    {
        public int MovieId { get; set; }
        public int Cantidad { get; set; }
    }
}
