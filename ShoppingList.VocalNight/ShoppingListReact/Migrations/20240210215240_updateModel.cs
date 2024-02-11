using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingListReact.Migrations
{
    /// <inheritdoc />
    public partial class updateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPickedUp",
                table: "ShoppingListItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "ShoppingListItems",
                columns: new[] { "Id", "IsPickedUp", "Name" },
                values: new object[] { 1, false, "Pasta" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShoppingListItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IsPickedUp",
                table: "ShoppingListItems");
        }
    }
}
