using Microsoft.Data.Entity;
using AspNetCorePostgreSQLDockerApp.Models;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class DockerCommandsDbContext : DbContext
    {
        public DbSet<DockerCommand> DockerCommands { get; set; }
    }
}