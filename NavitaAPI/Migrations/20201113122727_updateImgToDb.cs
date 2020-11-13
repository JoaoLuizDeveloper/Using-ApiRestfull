using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavitaAPI.Migrations
{
    public partial class updateImgToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "NumeroTombo",
                table: "Patrimonios",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Marcas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Marcas");

            migrationBuilder.AlterColumn<int>(
                name: "NumeroTombo",
                table: "Patrimonios",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
