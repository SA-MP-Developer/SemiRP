﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SemiRP;

namespace SemiRP.Migrations
{
    [DbContext(typeof(ServerDbContext))]
    [Migration("20200413232052_ItemsHeritage")]
    partial class ItemsHeritage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SemiRP.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastConnectionIP")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastConnectionTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SemiRP.Models.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ContainerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("OnSale")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("OwnerGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<bool>("Salable")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("OwnerGroupId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("SemiRP.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<uint>("Age")
                        .HasColumnType("int unsigned");

                    b.Property<int?>("InventoryId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<int?>("SpawnLocationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("InventoryId");

                    b.HasIndex("SpawnLocationId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("SemiRP.Models.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Containers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Container");
                });

            modelBuilder.Entity("SemiRP.Models.ContainerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MaxSpace")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ContainerTypes");
                });

            modelBuilder.Entity("SemiRP.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SemiRP.Models.GroupRank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CharacterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("ParentGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ParentGroupId");

                    b.ToTable("GroupRanks");
                });

            modelBuilder.Entity("SemiRP.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CurrentContainerId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CurrentContainerId");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");
                });

            modelBuilder.Entity("SemiRP.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("CharacterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("ParentPermissionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ParentPermissionId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("SemiRP.Models.SpawnLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Interior")
                        .HasColumnType("int");

                    b.Property<float>("RotX")
                        .HasColumnType("float");

                    b.Property<float>("RotY")
                        .HasColumnType("float");

                    b.Property<float>("RotZ")
                        .HasColumnType("float");

                    b.Property<int>("VirtualWorld")
                        .HasColumnType("int");

                    b.Property<float>("X")
                        .HasColumnType("float");

                    b.Property<float>("Y")
                        .HasColumnType("float");

                    b.Property<float>("Z")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("SpawnLocation");
                });

            modelBuilder.Entity("SemiRP.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ContainerId")
                        .HasColumnType("int");

                    b.Property<int>("IdVehicle")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("SemiRP.Models.ContainerHeritage.Inventory", b =>
                {
                    b.HasBaseType("SemiRP.Models.Container");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasIndex("OwnerId");

                    b.HasDiscriminator().HasValue("Inventory");
                });

            modelBuilder.Entity("SemiRP.Models.ItemHeritage.Phone", b =>
                {
                    b.HasBaseType("SemiRP.Models.Item");

                    b.Property<bool>("DefaultPhone")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Number")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue("Phone");
                });

            modelBuilder.Entity("SemiRP.Models.Building", b =>
                {
                    b.HasOne("SemiRP.Models.Container", "Container")
                        .WithMany()
                        .HasForeignKey("ContainerId");

                    b.HasOne("SemiRP.Models.Group", "OwnerGroup")
                        .WithMany()
                        .HasForeignKey("OwnerGroupId");

                    b.HasOne("SemiRP.Models.Character", "Owner")
                        .WithMany("BuildingOwner")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("SemiRP.Models.Character", b =>
                {
                    b.HasOne("SemiRP.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("SemiRP.Models.Container", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId");

                    b.HasOne("SemiRP.Models.SpawnLocation", "SpawnLocation")
                        .WithMany()
                        .HasForeignKey("SpawnLocationId");
                });

            modelBuilder.Entity("SemiRP.Models.Container", b =>
                {
                    b.HasOne("SemiRP.Models.ContainerType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("SemiRP.Models.Group", b =>
                {
                    b.HasOne("SemiRP.Models.Character", "Owner")
                        .WithMany("GroupOwner")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("SemiRP.Models.GroupRank", b =>
                {
                    b.HasOne("SemiRP.Models.Character", null)
                        .WithMany("GroupRanks")
                        .HasForeignKey("CharacterId");

                    b.HasOne("SemiRP.Models.Group", "ParentGroup")
                        .WithMany("ListRank")
                        .HasForeignKey("ParentGroupId");
                });

            modelBuilder.Entity("SemiRP.Models.Item", b =>
                {
                    b.HasOne("SemiRP.Models.Container", "CurrentContainer")
                        .WithMany("ListItems")
                        .HasForeignKey("CurrentContainerId");
                });

            modelBuilder.Entity("SemiRP.Models.Permission", b =>
                {
                    b.HasOne("SemiRP.Models.Account", null)
                        .WithMany("Perms")
                        .HasForeignKey("AccountId");

                    b.HasOne("SemiRP.Models.Character", null)
                        .WithMany("Perms")
                        .HasForeignKey("CharacterId");

                    b.HasOne("SemiRP.Models.Permission", "ParentPermission")
                        .WithMany("ChildPermissions")
                        .HasForeignKey("ParentPermissionId");
                });

            modelBuilder.Entity("SemiRP.Models.Vehicle", b =>
                {
                    b.HasOne("SemiRP.Models.Container", "Container")
                        .WithMany()
                        .HasForeignKey("ContainerId");

                    b.HasOne("SemiRP.Models.Character", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("SemiRP.Models.ContainerHeritage.Inventory", b =>
                {
                    b.HasOne("SemiRP.Models.Character", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
