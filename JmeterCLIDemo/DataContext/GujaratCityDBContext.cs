using Microsoft.EntityFrameworkCore;

namespace JmeterCLIDemo
{
    public class GujaratCityDBContext:DbContext
    {
        public GujaratCityDBContext(DbContextOptions<GujaratCityDBContext> options):base(options)
        {
        }

        public DbSet<District> Districts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
