using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User>Users { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
