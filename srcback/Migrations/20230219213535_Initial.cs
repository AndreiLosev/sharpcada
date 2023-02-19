using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace sharpcada.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    DeviceAddres = table.Column<int>(type: "integer", nullable: true),
                    DataAddres = table.Column<long>(type: "bigint", nullable: true),
                    FunctionCode = table.Column<byte>(type: "smallint", nullable: true),
                    Port = table.Column<int>(type: "integer", nullable: true),
                    Length = table.Column<int>(type: "integer", nullable: true),
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
                name: "DeviceParameterNetworkChannel",
                columns: table => new
                {
                    DeviceParametersId = table.Column<long>(type: "bigint", nullable: false),
                    NetworkChannelsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceParameterNetworkChannel", x => new { x.DeviceParametersId, x.NetworkChannelsId });
                    table.ForeignKey(
                        name: "FK_DeviceParameterNetworkChannel_DeviceParameters_DeviceParame~",
                        column: x => x.DeviceParametersId,
                        principalTable: "DeviceParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceParameterNetworkChannel_NetworkChannels_NetworkChanne~",
                        column: x => x.NetworkChannelsId,
                        principalTable: "NetworkChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NetworkChannelDeviceParameter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NetworkChannelId = table.Column<long>(type: "bigint", nullable: false),
                    DeviceParameterId = table.Column<long>(type: "bigint", nullable: false),
                    IndexNumber = table.Column<int>(type: "integer", nullable: false),
                    BitIndexNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkChannelDeviceParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkChannelDeviceParameter_DeviceParameters_DeviceParame~",
                        column: x => x.DeviceParameterId,
                        principalTable: "DeviceParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NetworkChannelDeviceParameter_NetworkChannels_NetworkChanne~",
                        column: x => x.NetworkChannelId,
                        principalTable: "NetworkChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceParameterNetworkChannel_NetworkChannelsId",
                table: "DeviceParameterNetworkChannel",
                column: "NetworkChannelsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceParameters_DeviceId",
                table: "DeviceParameters",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkChannelDeviceParameter_DeviceParameterId",
                table: "NetworkChannelDeviceParameter",
                column: "DeviceParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkChannelDeviceParameter_NetworkChannelId",
                table: "NetworkChannelDeviceParameter",
                column: "NetworkChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkChannels_DeviceId",
                table: "NetworkChannels",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceParameterNetworkChannel");

            migrationBuilder.DropTable(
                name: "NetworkChannelDeviceParameter");

            migrationBuilder.DropTable(
                name: "DeviceParameters");

            migrationBuilder.DropTable(
                name: "NetworkChannels");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
