using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? CategoryName { get; set; }
    }
}
