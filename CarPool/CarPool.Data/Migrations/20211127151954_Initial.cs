﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoogleAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    ApplicationRoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.CheckConstraint("Password_contains_space", "Password NOT LIKE '% %'");
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_ApplicationRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ban",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    BlockedOn = table.Column<DateTime>(nullable: true),
                    BlockedDue = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ban", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ban_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ImageLink = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePictures_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    AddedByUserId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    Feedback = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DriverId = table.Column<Guid>(nullable: false),
                    StartAddressId = table.Column<int>(nullable: false),
                    DestinationAddressId = table.Column<int>(nullable: false),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    DurationInMinutes = table.Column<int>(nullable: false),
                    Distance = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PassengersCount = table.Column<int>(nullable: false),
                    FreeSeats = table.Column<int>(nullable: false),
                    AdditionalComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_DestinationAddresses",
                        column: x => x.DestinationAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_ApplicationUsers",
                        column: x => x.DriverId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_StartAddresses",
                        column: x => x.StartAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    Color = table.Column<string>(nullable: false),
                    FuelConsumptionPerHundredKilometers = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVehicles_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPassengers",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPassengers", x => new { x.ApplicationUserId, x.TripId });
                    table.ForeignKey(
                        name: "FK_TripPassengerRelation_ApplicationUsers",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripPassengerRelation_Trips",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ApplicationRoles",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(5672), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(6087), null, false, null, "User" },
                    { 3, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(6102), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(6103), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 27, 15, 19, 53, 387, DateTimeKind.Utc).AddTicks(8294), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 11, 27, 15, 19, 53, 388, DateTimeKind.Utc).AddTicks(128), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 11, 27, 15, 19, 53, 388, DateTimeKind.Utc).AddTicks(160), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 11, 27, 15, 19, 53, 388, DateTimeKind.Utc).AddTicks(163), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(7198), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8782), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8823), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8826), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8840), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8843), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8828), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8834), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8836), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8838), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8845), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 11, 27, 15, 19, 53, 389, DateTimeKind.Utc).AddTicks(8847), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(1075), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 6, 1, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(4572), null, false, 44.432558m, 26.111871m, null, null },
                    { 2, 2, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(4502), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 7, 2, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(4575), null, false, 44.432558m, 26.111871m, null, null },
                    { 3, 3, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(4558), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 8, 3, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(4578), null, false, 44.432558m, 26.111871m, null, null },
                    { 4, 4, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(4563), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 9, 4, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(4581), null, false, 44.432558m, 26.111871m, null, null },
                    { 5, 5, new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(4566), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("3d256967-12f3-4b2f-85d2-883ebf8162cf"), 1, 2, new DateTime(2021, 11, 27, 15, 19, 53, 391, DateTimeKind.Utc).AddTicks(2419), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$F5eDmPfgPd5O6HKdtqMRIOtCceHeroxUU/0tlYvsnJ22PbixHtvxG", "+35920768005", "misha_m" },
                    { new Guid("91f614a8-f877-45b6-962a-35729b9467de"), 1, 1, new DateTime(2021, 11, 27, 15, 19, 54, 30, DateTimeKind.Utc).AddTicks(2007), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$DrujA09a/rwHcYiw7JBaF.oAtLTy01qm7dhZ2IzxvnKl77SFOvlXK", "+35924775508", "cicibar" },
                    { new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), 2, 2, new DateTime(2021, 11, 27, 15, 19, 53, 586, DateTimeKind.Utc).AddTicks(5942), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$q5/0C56U.PxzwKWnZMNGve/QDcdwcW.JyH/VjiqZsDLLQ3OHPlj9K", "+35924492877", "petio_p" },
                    { new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), 3, 2, new DateTime(2021, 11, 27, 15, 19, 53, 804, DateTimeKind.Utc).AddTicks(4720), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$D.7H6naOLe/4fsHpPnerXeDA5KacVyXnq.LpGn/s3zSoCBDRDWc0C", "+35922649764", "koksal" }
                });

            migrationBuilder.InsertData(
                table: "Ban",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), null, new DateTime(2021, 11, 27, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(3058), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageLink", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("3d256967-12f3-4b2f-85d2-883ebf8162cf"), new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(1392), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 3, new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(2231), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 2, new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(2210), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 4, new Guid("91f614a8-f877-45b6-962a-35729b9467de"), new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(2234), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 3, new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), new Guid("91f614a8-f877-45b6-962a-35729b9467de"), new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(8910), "(No feedback)", null, 5 },
                    { 1, new Guid("3d256967-12f3-4b2f-85d2-883ebf8162cf"), new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(7313), "Nice car", null, 4 },
                    { 2, new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), new Guid("3d256967-12f3-4b2f-85d2-883ebf8162cf"), new DateTime(2021, 11, 27, 15, 19, 53, 390, DateTimeKind.Utc).AddTicks(8873), "Bad person", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 15, "High price", new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(177), new DateTime(2021, 11, 27, 17, 19, 54, 228, DateTimeKind.Local).AddTicks(179), 2, 240, new Guid("3d256967-12f3-4b2f-85d2-883ebf8162cf"), 120, 2, null, 1, 15.21m, 3 },
                    { 13, "Good looking and friendly", new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(164), new DateTime(2021, 11, 27, 17, 19, 54, 228, DateTimeKind.Local).AddTicks(167), 4, 240, new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), 120, 2, null, 1, 10.13m, 3 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9675), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9677), 3, 240, new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), 120, 2, null, 1, 19.11m, 1 },
                    { 5, "Additional comments below", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9632), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9634), 4, 240, new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), 120, 2, null, 1, 0m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9626), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9627), 1, 240, new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9615), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9619), 2, 210, new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), 110, 2, null, 2, 0m, 4 },
                    { 16, "Im not alone", new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(183), new DateTime(2021, 11, 27, 17, 19, 54, 228, DateTimeKind.Local).AddTicks(184), 1, 240, new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(172), new DateTime(2021, 11, 27, 17, 19, 54, 228, DateTimeKind.Local).AddTicks(174), 3, 240, new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9668), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9670), 1, 240, new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), 120, 2, null, 1, 0m, 3 },
                    { 2, "NO SMOKING", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9494), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9536), 3, 240, new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), 120, 2, null, 1, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(188), new DateTime(2021, 11, 27, 17, 19, 54, 228, DateTimeKind.Local).AddTicks(190), 3, 240, new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), 120, 2, null, 1, 12.23m, 1 },
                    { 1, "(No comment)", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(5733), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(7848), 2, 340, new Guid("3d256967-12f3-4b2f-85d2-883ebf8162cf"), 90, 2, null, 2, 0m, 1 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9656), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9658), 2, 240, new Guid("3d256967-12f3-4b2f-85d2-883ebf8162cf"), 120, 2, null, 1, 0m, 1 },
                    { 12, "NO STOPS", new DateTime(2021, 11, 27, 15, 19, 54, 228, DateTimeKind.Utc).AddTicks(124), new DateTime(2021, 11, 27, 17, 19, 54, 228, DateTimeKind.Local).AddTicks(135), 3, 240, new Guid("91f614a8-f877-45b6-962a-35729b9467de"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9645), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9646), 4, 240, new Guid("91f614a8-f877-45b6-962a-35729b9467de"), 120, 2, null, 1, 0m, 2 },
                    { 9, "FAST FAST FAST", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9661), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9663), 3, 240, new Guid("3d256967-12f3-4b2f-85d2-883ebf8162cf"), 120, 2, null, 1, 0m, 2 },
                    { 7, "NO EATING", new DateTime(2021, 11, 27, 15, 19, 54, 227, DateTimeKind.Utc).AddTicks(9650), new DateTime(2021, 11, 27, 17, 19, 54, 227, DateTimeKind.Local).AddTicks(9652), 2, 240, new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), 120, 2, null, 1, 0m, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "DeletedOn", "FuelConsumptionPerHundredKilometers", "IsDeleted", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("3d256967-12f3-4b2f-85d2-883ebf8162cf"), "Red", new DateTime(2021, 11, 27, 15, 19, 54, 223, DateTimeKind.Utc).AddTicks(9188), null, 12.0, false, "Ferrari", null },
                    { 2, new Guid("6ec84e6b-0f7d-4190-bfdc-6f186e7e8629"), "Blue", new DateTime(2021, 11, 27, 15, 19, 54, 224, DateTimeKind.Utc).AddTicks(1286), null, 8.0, false, "Alfa Romeo", null },
                    { 4, new Guid("91f614a8-f877-45b6-962a-35729b9467de"), "Silver", new DateTime(2021, 11, 27, 15, 19, 54, 224, DateTimeKind.Utc).AddTicks(1387), null, 15.0, false, "BMW M5", null },
                    { 3, new Guid("8edada10-87eb-425e-857e-929dd60cd82d"), "Black", new DateTime(2021, 11, 27, 15, 19, 54, 224, DateTimeKind.Utc).AddTicks(1358), null, 10.0, false, "Mercedes S Class", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_AddressId",
                table: "ApplicationUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_ApplicationRoleId",
                table: "ApplicationUsers",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_Email",
                table: "ApplicationUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_PhoneNumber",
                table: "ApplicationUsers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_Username",
                table: "ApplicationUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ban_ApplicationUserId",
                table: "Ban",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_CountryId",
                table: "Cities",
                columns: new[] { "Name", "CountryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_ApplicationUserId",
                table: "ProfilePictures",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ApplicationUserId",
                table: "Ratings",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPassengers_TripId",
                table: "TripPassengers",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationAddressId",
                table: "Trips",
                column: "DestinationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverId",
                table: "Trips",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_StartAddressId",
                table: "Trips",
                column: "StartAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVehicles_ApplicationUserId",
                table: "UserVehicles",
                column: "ApplicationUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ban");

            migrationBuilder.DropTable(
                name: "GoogleAccount");

            migrationBuilder.DropTable(
                name: "ProfilePictures");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "TripPassengers");

            migrationBuilder.DropTable(
                name: "UserVehicles");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}