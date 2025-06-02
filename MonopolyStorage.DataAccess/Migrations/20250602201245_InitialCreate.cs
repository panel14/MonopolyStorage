using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonopolyStorage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Depth = table.Column<double>(type: "double precision", nullable: false),
                    Volume = table.Column<double>(type: "double precision", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProductionDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pallets", x => x.Id);
                    table.CheckConstraint("ValidDepth", "\"Depth\" > 0");
                    table.CheckConstraint("ValidHeight", "\"Height\" > 0");
                    table.CheckConstraint("ValidWidth", "\"Width\" > 0");
                });

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Depth = table.Column<double>(type: "double precision", nullable: false),
                    Volume = table.Column<double>(type: "double precision", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProductionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PalletId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Id);
                    table.CheckConstraint("ValidDepth", "\"Depth\" > 0");
                    table.CheckConstraint("ValidHeight", "\"Height\" > 0");
                    table.CheckConstraint("ValidWidth", "\"Width\" > 0");
                    table.ForeignKey(
                        name: "FK_Boxes_Pallets_PalletId",
                        column: x => x.PalletId,
                        principalTable: "Pallets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_PalletId",
                table: "Boxes",
                column: "PalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Pallets");
        }
    }
}
