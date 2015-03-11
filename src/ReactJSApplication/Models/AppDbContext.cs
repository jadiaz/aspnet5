using System;
using Microsoft.Data.Entity;

namespace ReactJsApplication.Models
{
    public class AppDbContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
    }
}