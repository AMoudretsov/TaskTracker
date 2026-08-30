using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskTracker.Infrastructure.Db.Migrations
{
    /// <inheritdoc />
    public partial class InitDbModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tsk");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:tsk-case-insensitive", "und-u-kf-upper-ks-level2,und-u-kf-upper-ks-level2,icu,False");

            migrationBuilder.CreateTable(
                name: "task_item",
                schema: "tsk",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false, collation: "tsk-case-insensitive"),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true, collation: "tsk-case-insensitive"),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_item", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_task_item_title",
                schema: "tsk",
                table: "task_item",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task_item",
                schema: "tsk");
        }
    }
}
