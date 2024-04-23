using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => new { x.UserId, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    ScopeUserId = table.Column<string>(type: "text", nullable: false),
                    ScopeName = table.Column<string>(type: "character varying(100)", nullable: false),
                    Color = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => new { x.ScopeUserId, x.ScopeName });
                    table.ForeignKey(
                        name: "FK_Icons_Scopes_ScopeUserId_ScopeName",
                        columns: x => new { x.ScopeUserId, x.ScopeName },
                        principalTable: "Scopes",
                        principalColumns: new[] { "UserId", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Secrets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ScopeUserId = table.Column<string>(type: "text", nullable: false),
                    ScopeName = table.Column<string>(type: "character varying(100)", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Secrets_Scopes_ScopeUserId_ScopeName",
                        columns: x => new { x.ScopeUserId, x.ScopeName },
                        principalTable: "Scopes",
                        principalColumns: new[] { "UserId", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Secrets_ScopeUserId_ScopeName",
                table: "Secrets",
                columns: new[] { "ScopeUserId", "ScopeName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "Secrets");

            migrationBuilder.DropTable(
                name: "Scopes");
        }
    }
}
