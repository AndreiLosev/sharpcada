using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcada.Migrations
{
    /// <inheritdoc />
    public partial class BitIndexNumber_swithc_to_short : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "BitIndexNumber",
                table: "DevParameterNetChannel",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BitIndexNumber",
                table: "DevParameterNetChannel",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }
    }
}
