using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    password = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "user_aggregate",
                columns: table => new
                {
                    user_aggregate_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    salt_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_aggregate", x => x.user_aggregate_id);
                    table.ForeignKey(
                        name: "FK_user_aggregate_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_salt",
                columns: table => new
                {
                    salt_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_aggregate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    salt = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_salt", x => x.salt_id);
                    table.ForeignKey(
                        name: "FK_user_salt_user_aggregate_user_aggregate_id",
                        column: x => x.user_aggregate_id,
                        principalTable: "user_aggregate",
                        principalColumn: "user_aggregate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_aggregate_salt_id",
                table: "user_aggregate",
                column: "salt_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_aggregate_user_id",
                table: "user_aggregate",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_salt_user_aggregate_id",
                table: "user_salt",
                column: "user_aggregate_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_aggregate_user_salt_salt_id",
                table: "user_aggregate",
                column: "salt_id",
                principalTable: "user_salt",
                principalColumn: "salt_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_aggregate_user_salt_salt_id",
                table: "user_aggregate");

            migrationBuilder.DropTable(
                name: "user_salt");

            migrationBuilder.DropTable(
                name: "user_aggregate");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
