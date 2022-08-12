using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdapterImec.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageId = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    CustomerScheme = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    CustomerValue = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    ProvidingCompanyScheme = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    ProvidingCompanyValue = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    MessageType = table.Column<string>(type: "varchar", maxLength: 20, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    DateReceived = table.Column<DateTime>(type: "timestamp", nullable: false),
                    FileContent = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
