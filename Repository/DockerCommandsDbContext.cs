using Microsoft.EntityFrameworkCore;
using AspNetCorePostgreSQLDockerApp.Models;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class DockerCommandsDbContext : DbContext
    {
        public DockerCommandsDbContext(DbContextOptions options) : base(options) { }
        public DbSet<DockerCommand> DockerCommands { get; set; }
    }
}