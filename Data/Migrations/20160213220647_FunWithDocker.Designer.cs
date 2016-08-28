using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AspNetCorePostgreSQLDockerApp.Repository;

namespace AspNetCorePostgreSQLDockerApp.Migrations
{
    [DbContext(typeof(DockerCommandsDbContext))]
    [Migration("20160213220647_FunWithDocker")]
    partial class FunWithDocker
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("AspNetCorePostgreSQLDockerApp.Models.DockerCommand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Command");

                    b.Property<string>("Description");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("AspNetCorePostgreSQLDockerApp.Models.DockerCommandExample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("DockerCommandId");

                    b.Property<string>("Example");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("AspNetCorePostgreSQLDockerApp.Models.DockerCommandExample", b =>
                {
                    b.HasOne("AspNetCorePostgreSQLDockerApp.Models.DockerCommand")
                        .WithMany()
                        .HasForeignKey("DockerCommandId");
                });
        }
    }
}
