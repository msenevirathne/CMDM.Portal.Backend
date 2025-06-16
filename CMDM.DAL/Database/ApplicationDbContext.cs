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
    }
}
