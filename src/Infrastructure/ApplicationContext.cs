using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User>Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Rental> Rentals { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .Property(d => d.Role)
                .HasConversion(new EnumToStringConverter<UserRole>());

            modelBuilder.Entity<Movie>()
             .HasOne(b => b.Category)
             .WithMany(c => c.Movie)
             .HasForeignKey(b => b.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            // relaciones rental -> movie / Cliente
            modelBuilder.Entity<Rental>()
                .HasOne(o => o.Movie)
                .WithMany() // una peli puede estar en varios pedidos
                .HasForeignKey(o => o.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(o => o.Client)
                .WithMany() // un cliente puede tener varios pedidos
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
