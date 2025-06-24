using CMDM.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CMDM.DAL.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<CustomerReference> CustomerReferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerReference>()
                .HasOne(cr => cr.CustomerMaster)
                .WithMany(cm => cm.CustomerReferences)
                .HasForeignKey(cr => cr.ParentCustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Or Cascade, depending on logic

            //modelBuilder.Entity<CustomerReference>()
            //.Property(cr => cr.ParentCustomerId)
            //.HasColumnName("CustomerMasterId");
        }
    }
}
