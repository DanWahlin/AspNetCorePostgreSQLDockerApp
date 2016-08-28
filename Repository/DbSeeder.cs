using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AspNetCorePostgreSQLDockerApp.Models;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class DbSeeder
    {
        readonly DockerCommandsDbContext _context;
        readonly IDockerCommandsRepository _dockerCommandsRepository;
        readonly ILogger _logger;

        public DbSeeder(DockerCommandsDbContext context, IDockerCommandsRepository repo, ILoggerFactory loggerFactory)
        {
            _context = context;
            _dockerCommandsRepository = repo;
            _logger = loggerFactory.CreateLogger("DbSeederLogger");
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            //Based on EF team's example at https://github.com/aspnet/MusicStore/blob/dev/samples/MusicStore/Models/SampleData.cs
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<DockerCommandsDbContext>();

                if (await db.Database.EnsureCreatedAsync())
                {
                    if (!await db.DockerCommands.AnyAsync()) {
                      await InsertSampleData(db);
                    }
                }
            }
        }

        public async Task InsertSampleData(DockerCommandsDbContext db)
        {
            var commands = GetDockerCommands();
            db.DockerCommands.AddRange(commands);

            try
            {
              await db.SaveChangesAsync();
            }
            catch (Exception exp)
            {
              _logger.LogError($"Error in {nameof(DbSeeder)}: " + exp.Message);
            }

        }

        private List<DockerCommand> GetDockerCommands()
        {
            var cmd1 = new DockerCommand {
                Command = "run",
                Description = "Runs a Docker container",
                Examples  = new List<DockerCommandExample> {
                    new DockerCommandExample {
                        Example = "docker run imageName",
                        Description = "Creates a running container from the image. Pulls it from Docker Hub if the image is not local"
                    },
                    new DockerCommandExample {
                        Example = "docker run -d -p 8080:3000 imageName",
                        Description = "Runs a container in 'daemon' mode with an external port of 8080 and a container port of 3000."
                    }
                }
            };

            var cmd2 = new DockerCommand {
                Command = "ps",
                Description = "Lists containers",
                Examples  = new List<DockerCommandExample> {
                    new DockerCommandExample {
                        Example = "docker ps",
                        Description = "Lists all running containers"
                    },
                    new DockerCommandExample {
                        Example = "docker ps -a",
                        Description = "Lists all containers (even if they are not running)"
                    }
                }
            };

            return new List<DockerCommand> { cmd1, cmd2 };
        }
    }
}