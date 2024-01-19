using Microsoft.EntityFrameworkCore;
using RealEstateServices.Models;

namespace RealEstateServices.DBContexts
{
    public class RealEstateContext: DbContext
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Course");
            modelBuilder.Entity<Product>().ToTable("Enrollment");
            modelBuilder.Entity<Landlord>().ToTable("Student");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Landlord> Landlord { get; set;}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=VINHLUONG-LAPTO\\VINHLUONG_SERVER;Database=RealEstateDb;User Id=sa;Password=12345678x@X");
        //}
    }
}
