using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenSpend.Data.Migrations;

/// <inheritdoc />
public partial class TimestampBehavior : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Groups",
            type: "timestamp without time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Groups",
            type: "timestamp without time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "GroupMembers",
            type: "timestamp without time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "GroupMembers",
            type: "timestamp without time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Friendships",
            type: "timestamp without time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Friendships",
            type: "timestamp without time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Groups",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp without time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Groups",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp without time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "GroupMembers",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp without time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "GroupMembers",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp without time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Friendships",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp without time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Friendships",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp without time zone");
    }
}
