using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcada.Migrations
{
    /// <inheritdoc />
    public partial class valut_to_frloat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<float>(
            //     name: "Value",
            //     table: "Meterages",
            //     type: "real",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "text");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Meterages");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Meterages",
                type: "real",
                nullable: false);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Meterages",
                type: "text",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
