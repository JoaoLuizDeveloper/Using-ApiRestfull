using Microsoft.EntityFrameworkCore.Migrations;

namespace NavitaAPI.Migrations
{
    public partial class updateintTomboToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumeroTombo",
                table: "Patrimonios",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "NumeroTombo",
                table: "Patrimonios",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
