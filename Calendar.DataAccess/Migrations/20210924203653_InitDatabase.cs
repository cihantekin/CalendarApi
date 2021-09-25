using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.DataAccess.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Organizer = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    EventsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => new { x.EventsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_EventUser_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationTime", "DeletionTime", "IsDeleted", "LastModificationTime", "Name" },
                values: new object[,]
                {
                    { new Guid("f97e47e8-6070-4b48-9f8a-a678bb630434"), new DateTime(2021, 9, 24, 23, 36, 52, 565, DateTimeKind.Local).AddTicks(1189), null, false, null, "Sam" },
                    { new Guid("bebf9a43-b78d-4ec4-a953-f9eafde87041"), new DateTime(2021, 9, 24, 23, 36, 52, 566, DateTimeKind.Local).AddTicks(5299), null, false, null, "Any" },
                    { new Guid("5b6b6abe-74f3-4309-b513-b07a840775aa"), new DateTime(2021, 9, 24, 23, 36, 52, 566, DateTimeKind.Local).AddTicks(5332), null, false, null, "Jay" },
                    { new Guid("bb45851f-100e-443e-b305-da0409c8c8d3"), new DateTime(2021, 9, 24, 23, 36, 52, 566, DateTimeKind.Local).AddTicks(5335), null, false, null, "Samuel" },
                    { new Guid("f87cbda3-e5b1-48a2-a80d-f53dc4af9e1b"), new DateTime(2021, 9, 24, 23, 36, 52, 566, DateTimeKind.Local).AddTicks(5338), null, false, null, "Mike" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_MembersId",
                table: "EventUser",
                column: "MembersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
