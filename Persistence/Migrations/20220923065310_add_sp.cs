using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class add_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

            var sqlQuery = Path.Combine(directory, "sp_final_insert.sql");
            if (File.Exists(sqlQuery))
                migrationBuilder.Sql(File.ReadAllText(sqlQuery));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[FinalTable]");
        }
    }
}
