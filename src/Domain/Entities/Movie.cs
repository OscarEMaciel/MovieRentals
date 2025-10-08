using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required]
        [MaxLength(100)]

        public string Title { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public bool IsDeleted { get; set; } = false; //para hacer la baja logica


        public int CategoryId { get; set; }

        public Category? category { get; set; }




        public Movie(string title, decimal price, int stock, int categoryId)
        {
            Title = title;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

    }
}
