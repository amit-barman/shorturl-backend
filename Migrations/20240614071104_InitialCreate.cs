using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shorturl.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    realUrl = table.Column<string>(type: "TEXT", nullable: true),
                    shortUrl = table.Column<string>(type: "TEXT", nullable: true),
                    creationTime = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
