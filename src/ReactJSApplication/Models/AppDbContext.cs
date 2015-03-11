using System;
using Microsoft.Data.Entity;

namespace MvcApplication.Models
{
    public class AppDbContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
    }
}