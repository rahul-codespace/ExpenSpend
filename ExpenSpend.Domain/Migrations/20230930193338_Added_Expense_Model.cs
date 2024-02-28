using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenSpend.Data.Migrations;

/// <inheritdoc />
public partial class Added_Expense_Model : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Expenses",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Title = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                PaidById = table.Column<Guid>(type: "uuid", nullable: false),
                Amount = table.Column<double>(type: "double precision", nullable: false),
                SplitAs = table.Column<int>(type: "integer", nullable: false),
                IsSettled = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Expenses", x => x.Id);
                table.ForeignKey(
                    name: "FK_Expenses_AspNetUsers_PaidById",
                    column: x => x.PaidById,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Expenses_Groups_GroupId",
                    column: x => x.GroupId,
                    principalTable: "Groups",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Payments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                OwenedById = table.Column<Guid>(type: "uuid", nullable: false),
                ExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
                Amount = table.Column<double>(type: "double precision", nullable: false),
                IsSettled = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Payments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Payments_AspNetUsers_OwenedById",
                    column: x => x.OwenedById,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Payments_Expenses_ExpenseId",
                    column: x => x.ExpenseId,
                    principalTable: "Expenses",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Expenses_GroupId",
            table: "Expenses",
            column: "GroupId");

        migrationBuilder.CreateIndex(
            name: "IX_Expenses_PaidById",
            table: "Expenses",
            column: "PaidById");

        migrationBuilder.CreateIndex(
            name: "IX_Payments_ExpenseId",
            table: "Payments",
            column: "ExpenseId");

        migrationBuilder.CreateIndex(
            name: "IX_Payments_OwenedById",
            table: "Payments",
            column: "OwenedById");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Payments");

        migrationBuilder.DropTable(
            name: "Expenses");
    }
}
