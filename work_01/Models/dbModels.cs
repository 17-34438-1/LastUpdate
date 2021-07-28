using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace work_01.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public string ImageFile { get; set; }
    }
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
