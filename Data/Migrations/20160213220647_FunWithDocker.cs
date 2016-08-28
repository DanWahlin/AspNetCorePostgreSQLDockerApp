using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCorePostgreSQLDockerApp.Migrations
{
    public partial class FunWithDocker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DockerCommand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Command = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerCommand", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "DockerCommandExample",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Description = table.Column<string>(nullable: true),
                    DockerCommandId = table.Column<int>(nullable: true),
                    Example = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerCommandExample", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DockerCommandExample_DockerCommand_DockerCommandId",
                        column: x => x.DockerCommandId,
                        principalTable: "DockerCommand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("DockerCommandExample");
            migrationBuilder.DropTable("DockerCommand");
        }
    }
}
