using Microsoft.EntityFrameworkCore;

namespace JmeterCLIDemo
{
    public class GujaratCityDBContext:DbContext
    {
        public GujaratCityDBContext(DbContextOptions<GujaratCityDBContext> options):base(options)
        {
        }

        public DbSet<District> GujaratDistricts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<District>().Property(o=>o.ID).HasDefaultValueSql("NEWSEQUENTIALID()").IsRequired();
        }
    }
}
