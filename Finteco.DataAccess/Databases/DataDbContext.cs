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
        public DbSet<DeploymentTask> DeploymentTasks { get; set; }
        public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }
        public DbSet<ImplementationTask> ImplementationTasks { get; set; }
    }
}
