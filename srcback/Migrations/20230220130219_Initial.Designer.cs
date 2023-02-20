﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using sharpcada.Data;

#nullable disable

namespace sharpcada.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230220130219_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DeviceParameterNetworkChannel", b =>
                {
                    b.Property<long>("DeviceParametersId")
                        .HasColumnType("bigint");

                    b.Property<long>("NetworkChannelsId")
                        .HasColumnType("bigint");

                    b.HasKey("DeviceParametersId", "NetworkChannelsId");

                    b.HasIndex("NetworkChannelsId");

                    b.ToTable("DeviceParameterNetworkChannel");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.Device", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IpAddres")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("Protocol")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.DeviceParameter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<float>("CastB")
                        .HasColumnType("real");

                    b.Property<float>("CastK")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.Property<string>("Unit")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceParameters");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.Meterage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("DeviceParameterID")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DeviceParameterID");

                    b.ToTable("Meterages");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.NetworkChannel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("NetworkChannels");

                    b.HasDiscriminator<string>("Discriminator").HasValue("NetworkChannel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("sharpcada.Data.Entities.NetworkChannelDeviceParameter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("BitIndexNumber")
                        .HasColumnType("integer");

                    b.Property<long>("DeviceParameterId")
                        .HasColumnType("bigint");

                    b.Property<int>("IndexNumber")
                        .HasColumnType("integer");

                    b.Property<long>("NetworkChannelId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DeviceParameterId");

                    b.HasIndex("NetworkChannelId");

                    b.ToTable("NetworkChannelDeviceParameter");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.ModbusChannel", b =>
                {
                    b.HasBaseType("sharpcada.Data.Entities.NetworkChannel");

                    b.Property<long>("DataAddres")
                        .HasColumnType("bigint");

                    b.Property<int>("DeviceAddres")
                        .HasColumnType("integer");

                    b.Property<byte>("FunctionCode")
                        .HasColumnType("smallint");

                    b.Property<int?>("Length")
                        .HasColumnType("integer");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("ModbusChannel");
                });

            modelBuilder.Entity("DeviceParameterNetworkChannel", b =>
                {
                    b.HasOne("sharpcada.Data.Entities.DeviceParameter", null)
                        .WithMany()
                        .HasForeignKey("DeviceParametersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sharpcada.Data.Entities.NetworkChannel", null)
                        .WithMany()
                        .HasForeignKey("NetworkChannelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("sharpcada.Data.Entities.DeviceParameter", b =>
                {
                    b.HasOne("sharpcada.Data.Entities.Device", "Device")
                        .WithMany("Parameters")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.Meterage", b =>
                {
                    b.HasOne("sharpcada.Data.Entities.DeviceParameter", null)
                        .WithMany("Meterages")
                        .HasForeignKey("DeviceParameterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("sharpcada.Data.Entities.NetworkChannel", b =>
                {
                    b.HasOne("sharpcada.Data.Entities.Device", "Device")
                        .WithMany("NetworkChannels")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.NetworkChannelDeviceParameter", b =>
                {
                    b.HasOne("sharpcada.Data.Entities.DeviceParameter", "DeviceParameter")
                        .WithMany("ParameterChannels")
                        .HasForeignKey("DeviceParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sharpcada.Data.Entities.NetworkChannel", "NetworkChannel")
                        .WithMany("ChannelParameters")
                        .HasForeignKey("NetworkChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceParameter");

                    b.Navigation("NetworkChannel");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.Device", b =>
                {
                    b.Navigation("NetworkChannels");

                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.DeviceParameter", b =>
                {
                    b.Navigation("Meterages");

                    b.Navigation("ParameterChannels");
                });

            modelBuilder.Entity("sharpcada.Data.Entities.NetworkChannel", b =>
                {
                    b.Navigation("ChannelParameters");
                });
#pragma warning restore 612, 618
        }
    }
}