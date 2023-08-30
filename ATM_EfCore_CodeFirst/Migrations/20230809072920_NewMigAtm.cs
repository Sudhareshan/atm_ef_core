using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_EfCore_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class NewMigAtm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transations_Customers_Bank_CustomersAccountNumber",
                table: "Transations");

            migrationBuilder.DropIndex(
                name: "IX_Transations_Bank_CustomersAccountNumber",
                table: "Transations");

            migrationBuilder.DropColumn(
                name: "Bank_CustomersAccountNumber",
                table: "Transations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bank_CustomersAccountNumber",
                table: "Transations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transations_Bank_CustomersAccountNumber",
                table: "Transations",
                column: "Bank_CustomersAccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Transations_Customers_Bank_CustomersAccountNumber",
                table: "Transations",
                column: "Bank_CustomersAccountNumber",
                principalTable: "Customers",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
