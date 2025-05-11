using Finteco_Core.Domain.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Finteco.DataAccess.Databases
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyDatabase");
        }
        public DbSet<BaseTask> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BaseTask>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<ImplementationTask>("Implementation")
                .HasValue<DeploymentTask>("Deployment")
                .HasValue<MaintenanceTask>("Maintenance");

            modelBuilder.Entity<ImplementationTask>()
                .Property(t => t.TaskDescription)
                .HasMaxLength(1000);

            modelBuilder.Entity<DeploymentTask>()
                .Property(t => t.DeploymentScope)
                .HasMaxLength(400);

            modelBuilder.Entity<MaintenanceTask>()
                .Property(t => t.ServiceList)
                .HasMaxLength(400);

            modelBuilder.Entity<MaintenanceTask>()
                .Property(t => t.ServerList)
                .HasMaxLength(400);
        }
    }
}
