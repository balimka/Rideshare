﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class initial : Migration
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
                name: "Bans",
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
                    table.PrimaryKey("PK_Bans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bans_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inboxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Seen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inboxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inboxes_ApplicationUsers_ApplicationUserId",
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
                    TripId = table.Column<int>(nullable: false),
                    IsReport = table.Column<bool>(nullable: false),
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
                    CreatedOn = table.Column<DateTime>(nullable: false)
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
                    { 1, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(6830), null, false, null, "Admin" },
                    { 2, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(7281), null, false, null, "User" },
                    { 3, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(7294), null, false, null, "Banned" },
                    { 4, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(7296), null, false, null, "NotConfirmed" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 4, 15, 12, 56, 13, DateTimeKind.Utc).AddTicks(8744), null, false, null, "Bulgaria" },
                    { 2, new DateTime(2021, 12, 4, 15, 12, 56, 14, DateTimeKind.Utc).AddTicks(632), null, false, null, "Turkey" },
                    { 3, new DateTime(2021, 12, 4, 15, 12, 56, 14, DateTimeKind.Utc).AddTicks(662), null, false, null, "Greece" },
                    { 4, new DateTime(2021, 12, 4, 15, 12, 56, 14, DateTimeKind.Utc).AddTicks(665), null, false, null, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(7954), null, false, null, "Sofia" },
                    { 2, 1, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9585), null, false, null, "Plovdiv" },
                    { 3, 1, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9627), null, false, null, "Varna" },
                    { 4, 2, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9630), null, false, null, "Istanbul" },
                    { 9, 2, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9645), null, false, null, "Odrin" },
                    { 10, 2, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9649), null, false, null, "Ankara" },
                    { 5, 3, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9632), null, false, null, "Athens" },
                    { 6, 3, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9640), null, false, null, "Thessaloniki" },
                    { 7, 3, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9642), null, false, null, "Patras" },
                    { 8, 4, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9644), null, false, null, "Yash" },
                    { 11, 4, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9651), null, false, null, "Bucharest" },
                    { 12, 4, new DateTime(2021, 12, 4, 15, 12, 56, 15, DateTimeKind.Utc).AddTicks(9653), null, false, null, "Craiova" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "CreatedOn", "DeletedOn", "IsDeleted", "Latitude", "Longitude", "ModifiedOn", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(1784), null, false, 42.6860436m, 23.320311m, null, "Vasil Levski 14" },
                    { 6, 1, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(5588), null, false, 44.432558m, 26.111871m, null, null },
                    { 2, 2, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(5515), null, false, 42.1382775m, 24.7604295m, null, "blv. Iztochen 23" },
                    { 7, 2, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(5591), null, false, 44.432558m, 26.111871m, null, null },
                    { 3, 3, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(5574), null, false, 41.022079m, 28.9483964m, null, "blv. Halic 12" },
                    { 8, 3, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(5595), null, false, 44.432558m, 26.111871m, null, null },
                    { 4, 4, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(5579), null, false, 37.9916167m, 23.7363294m, null, "blv. Zeus 12" },
                    { 9, 4, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(5598), null, false, 44.432558m, 26.111871m, null, null },
                    { 5, 5, new DateTime(2021, 12, 4, 15, 12, 56, 16, DateTimeKind.Utc).AddTicks(5582), null, false, 44.432558m, 26.111871m, null, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "ApplicationRoleId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOn", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("12d14010-8774-4796-bbf2-a622979aa488"), 1, 2, new DateTime(2021, 12, 4, 15, 12, 56, 17, DateTimeKind.Utc).AddTicks(874), "mishkov@misho.com", true, "Misho", "Mishkov", null, "$2a$11$l9XYceXIH/KTzYSemZ66AOw.sgpx6vMZd7sB.j38yqRhD7yeN9dNC", "+35920768005", "misha_m" },
                    { new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), 1, 1, new DateTime(2021, 12, 4, 15, 12, 56, 600, DateTimeKind.Utc).AddTicks(4035), "indebt@greece.gov", true, "Nikolaos", "Tsitsibaris", null, "$2a$11$zz2xoU0NulNin0P1.3sN4OZndSpntnr4XDqvQE6ZMt3pzFD5QsQC.", "+35924775508", "cicibar" },
                    { new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), 2, 2, new DateTime(2021, 12, 4, 15, 12, 56, 211, DateTimeKind.Utc).AddTicks(6977), "petio@mvc.net", true, "Peter", "Petrov", null, "$2a$11$4j.rKdmTSzv3u4.eHFOA4eh4g3R6XE23br4IC3Z8Czda5NcZsEgNS", "+35924492877", "petio_p" },
                    { new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), 3, 2, new DateTime(2021, 12, 4, 15, 12, 56, 404, DateTimeKind.Utc).AddTicks(466), "koksal@asd.tr", true, "Koksal", "Baba", null, "$2a$11$xU8NedJT8MRKsVkTlf6zau2w.dPfqVjEugS5fLElv4Ue491l4/ace", "+35922649764", "koksal" }
                });

            migrationBuilder.InsertData(
                table: "Bans",
                columns: new[] { "Id", "ApplicationUserId", "BlockedDue", "BlockedOn", "CreatedOn", "ModifiedOn", "Reason" },
                values: new object[] { 2, new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), null, new DateTime(2021, 12, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 12, 4, 15, 12, 56, 791, DateTimeKind.Utc).AddTicks(8154), null, null });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "Id", "ApplicationUserId", "CreatedOn", "DeletedOn", "ImageLink", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, new Guid("12d14010-8774-4796-bbf2-a622979aa488"), new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(1285), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 3, new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(2277), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 4, new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(2280), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null },
                    { 2, new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(2219), null, "https://ik.imagekit.io/hb0rsbgap4f2/profilepicture_qVGMALiir.png?updatedAt=1637784974343", false, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "AddedByUserId", "ApplicationUserId", "CreatedOn", "Feedback", "IsReport", "ModifiedOn", "TripId", "Value" },
                values: new object[,]
                {
                    { 1, new Guid("12d14010-8774-4796-bbf2-a622979aa488"), new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(3552), "Nice car", false, null, 1, 4 },
                    { 3, new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(5493), "(No feedback)", false, null, 5, 5 },
                    { 2, new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), new Guid("12d14010-8774-4796-bbf2-a622979aa488"), new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(5453), "Bad person", false, null, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "AdditionalComment", "CreatedOn", "DepartureTime", "DestinationAddressId", "Distance", "DriverId", "DurationInMinutes", "FreeSeats", "ModifiedOn", "PassengersCount", "Price", "StartAddressId" },
                values: new object[,]
                {
                    { 15, "High price", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(7377), new DateTime(2021, 12, 4, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(7463), 2, 240, new Guid("12d14010-8774-4796-bbf2-a622979aa488"), 120, 2, null, 1, 15.21m, 3 },
                    { 13, "Good looking and friendly", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(7361), new DateTime(2021, 12, 4, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(7364), 4, 240, new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), 120, 2, null, 1, 10.13m, 3 },
                    { 11, "NO SMOKING NO FOOD", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6856), new DateTime(2021, 12, 4, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6857), 3, 240, new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), 120, 2, null, 1, 19.11m, 1 },
                    { 5, "Additional comments below", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6809), new DateTime(2021, 12, 6, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6811), 4, 240, new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), 120, 2, null, 1, 0m, 1 },
                    { 4, "Long comment here", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6802), new DateTime(2021, 12, 6, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6804), 1, 240, new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), 120, 2, null, 1, 0m, 4 },
                    { 3, "NO SMOKING", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6791), new DateTime(2021, 12, 6, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6795), 2, 210, new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), 110, 2, null, 2, 0m, 4 },
                    { 16, "Im not alone", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(7477), new DateTime(2021, 12, 4, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(7479), 1, 240, new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), 120, 2, null, 2, 21.21m, 2 },
                    { 14, "Fast car", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(7370), new DateTime(2021, 12, 4, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(7372), 3, 240, new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), 120, 2, null, 1, 10.11m, 1 },
                    { 10, "No pets", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6850), new DateTime(2021, 12, 4, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6851), 1, 240, new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), 120, 2, null, 1, 0m, 3 },
                    { 7, "NO EATING", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6830), new DateTime(2021, 12, 6, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6832), 2, 240, new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), 120, 2, null, 1, 0m, 4 },
                    { 2, "NO SMOKING", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6694), new DateTime(2021, 12, 6, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6737), 3, 240, new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), 120, 2, null, 2, 0m, 2 },
                    { 17, "No kids", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(7484), new DateTime(2021, 12, 4, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(7486), 3, 240, new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), 120, 2, null, 1, 12.23m, 1 },
                    { 1, "(No comment)", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(2164), new DateTime(2021, 12, 6, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(4975), 2, 340, new Guid("12d14010-8774-4796-bbf2-a622979aa488"), 90, 2, null, 2, 0m, 1 },
                    { 12, "NO STOPS", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(7321), new DateTime(2021, 12, 4, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(7332), 3, 240, new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), 120, 2, null, 1, 15.55m, 4 },
                    { 6, "follow me on twitter", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6823), new DateTime(2021, 12, 6, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6825), 4, 240, new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), 120, 2, null, 1, 0m, 2 },
                    { 8, "CHEAP AND FAST", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6836), new DateTime(2021, 12, 6, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6837), 2, 240, new Guid("12d14010-8774-4796-bbf2-a622979aa488"), 120, 2, null, 1, 0m, 1 },
                    { 9, "FAST FAST FAST", new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(6842), new DateTime(2021, 12, 4, 17, 12, 56, 792, DateTimeKind.Local).AddTicks(6844), 3, 240, new Guid("12d14010-8774-4796-bbf2-a622979aa488"), 120, 2, null, 1, 0m, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserVehicles",
                columns: new[] { "Id", "ApplicationUserId", "Color", "CreatedOn", "DeletedOn", "FuelConsumptionPerHundredKilometers", "IsDeleted", "Model", "ModifiedOn" },
                values: new object[,]
                {
                    { 2, new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), "Blue", new DateTime(2021, 12, 4, 15, 12, 56, 788, DateTimeKind.Utc).AddTicks(4751), null, 8.0, false, "Alfa Romeo", null },
                    { 4, new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), "Silver", new DateTime(2021, 12, 4, 15, 12, 56, 788, DateTimeKind.Utc).AddTicks(4859), null, 15.0, false, "BMW M5", null },
                    { 1, new Guid("12d14010-8774-4796-bbf2-a622979aa488"), "Red", new DateTime(2021, 12, 4, 15, 12, 56, 788, DateTimeKind.Utc).AddTicks(2632), null, 12.0, false, "Ferrari", null },
                    { 3, new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), "Black", new DateTime(2021, 12, 4, 15, 12, 56, 788, DateTimeKind.Utc).AddTicks(4831), null, 10.0, false, "Mercedes S Class", null }
                });

            migrationBuilder.InsertData(
                table: "TripPassengers",
                columns: new[] { "ApplicationUserId", "TripId", "CreatedOn" },
                values: new object[,]
                {
                    { new Guid("12d14010-8774-4796-bbf2-a622979aa488"), 1, new DateTime(2021, 12, 4, 15, 12, 56, 792, DateTimeKind.Utc).AddTicks(9167) },
                    { new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), 1, new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(106) },
                    { new Guid("b16cbebd-e9c9-47af-a3ab-e00f6a1fe85c"), 2, new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(83) },
                    { new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), 2, new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(108) },
                    { new Guid("3e444ff2-9154-456c-8e6d-50d79cfd390f"), 3, new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(105) },
                    { new Guid("fcd795d6-cbfb-4bb6-9df2-a284e3c436f4"), 3, new DateTime(2021, 12, 4, 15, 12, 56, 793, DateTimeKind.Utc).AddTicks(112) }
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
                name: "IX_Bans_ApplicationUserId",
                table: "Bans",
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
                name: "IX_Inboxes_ApplicationUserId",
                table: "Inboxes",
                column: "ApplicationUserId");

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
                name: "Bans");

            migrationBuilder.DropTable(
                name: "GoogleAccount");

            migrationBuilder.DropTable(
                name: "Inboxes");

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