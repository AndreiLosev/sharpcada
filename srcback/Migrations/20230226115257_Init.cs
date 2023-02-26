using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace sharpcada.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IpAddres = table.Column<string>(type: "text", nullable: false),
                    Protocol = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceParameters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    CastK = table.Column<float>(type: "real", nullable: false),
                    CastB = table.Column<float>(type: "real", nullable: false),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceParameters_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NetworkChannels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkChannels_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meterages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    DeviceParameterID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meterages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meterages_DeviceParameters_DeviceParameterID",
                        column: x => x.DeviceParameterID,
                        principalTable: "DeviceParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevParameterNetChannel",
                columns: table => new
                {
                    DeviceParameterId = table.Column<long>(type: "bigint", nullable: false),
                    NetworkChannelId = table.Column<long>(type: "bigint", nullable: false),
                    IndexNumber = table.Column<int>(type: "integer", nullable: false),
                    BitIndexNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevParameterNetChannel", x => new { x.DeviceParameterId, x.NetworkChannelId });
                    table.ForeignKey(
                        name: "FK_DevParameterNetChannel_DeviceParameters_DeviceParameterId",
                        column: x => x.DeviceParameterId,
                        principalTable: "DeviceParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevParameterNetChannel_NetworkChannels_NetworkChannelId",
                        column: x => x.NetworkChannelId,
                        principalTable: "NetworkChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModbusChannels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DeviceAddres = table.Column<int>(type: "integer", nullable: false),
                    DataAddres = table.Column<long>(type: "bigint", nullable: false),
                    FunctionCode = table.Column<byte>(type: "smallint", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: true),
                    ByteOrder = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModbusChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModbusChannels_NetworkChannels_Id",
                        column: x => x.Id,
                        principalTable: "NetworkChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceParameters_DeviceId",
                table: "DeviceParameters",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DevParameterNetChannel_NetworkChannelId",
                table: "DevParameterNetChannel",
                column: "NetworkChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Meterages_DeviceParameterID",
                table: "Meterages",
                column: "DeviceParameterID");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkChannels_DeviceId",
                table: "NetworkChannels",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevParameterNetChannel");

            migrationBuilder.DropTable(
                name: "Meterages");

            migrationBuilder.DropTable(
                name: "ModbusChannels");

            migrationBuilder.DropTable(
                name: "DeviceParameters");

            migrationBuilder.DropTable(
                name: "NetworkChannels");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
