using Microsoft.EntityFrameworkCore;
using Car_Manufacturing.Models;

namespace Car_Manufacturing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSets for your entities
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<InventoryModel> InventoryModels { get; set; }
        public DbSet<SupplierModel> Suppliers { get; set; }
        public DbSet<QualityReport> QualityReports { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        // Override OnModelCreating to configure primary keys and other constraints
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CarModel>().HasNoKey();
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Configure relationships if needed (e.g., QualityReport -> CarModel)
        //    modelBuilder.Entity<QualityReport>()
        //        .HasOne(q => q.CarModel)
        //        .WithMany() // A CarModel can have multiple QualityReports
        //        .HasForeignKey(q => q.ModelId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
