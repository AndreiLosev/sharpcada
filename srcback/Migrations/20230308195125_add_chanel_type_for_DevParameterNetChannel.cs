using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcada.Migrations
{
    /// <inheritdoc />
    public partial class add_chanel_type_for_DevParameterNetChannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "ChannelType",
                table: "DevParameterNetChannel",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelType",
                table: "DevParameterNetChannel");
        }
    }
}
