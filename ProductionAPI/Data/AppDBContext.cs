using Microsoft.EntityFrameworkCore;
using ProductionAPI.Data.Models;

namespace ProductionAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {
        
        }

        
        //public DbSet<ShippingRelease> FR_Final_Release { get; set; } 
        //public DbSet<ShippingReleaseNew> FR_Final_Release { get; set; } = default;


    }
}
