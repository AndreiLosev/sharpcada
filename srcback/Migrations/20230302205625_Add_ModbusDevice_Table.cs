using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcada.Migrations
{
    /// <inheritdoc />
    public partial class Add_ModbusDevice_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ByteOrder",
                table: "ModbusChannels");

            migrationBuilder.DropColumn(
                name: "DeviceAddres",
                table: "ModbusChannels");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "ModbusChannels");

            migrationBuilder.CreateTable(
                name: "ModbusDevice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DeviceAddres = table.Column<int>(type: "integer", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    ByteOrder = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModbusDevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModbusDevice_Devices_Id",
                        column: x => x.Id,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModbusDevice");

            migrationBuilder.AddColumn<byte>(
                name: "ByteOrder",
                table: "ModbusChannels",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "DeviceAddres",
                table: "ModbusChannels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "ModbusChannels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
