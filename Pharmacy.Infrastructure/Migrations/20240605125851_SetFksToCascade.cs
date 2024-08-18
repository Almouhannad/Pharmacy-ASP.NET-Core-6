using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    public partial class SetFksToCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_Patient",
                table: "Case");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Category",
                table: "Medicine");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineCase_Case",
                table: "MedicineCase");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineCase_Medicine",
                table: "MedicineCase");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineIngredient_Ingredient",
                table: "MedicineIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineIngredient_Medicine",
                table: "MedicineIngredient");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_Patient",
                table: "Case",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Category",
                table: "Medicine",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineCase_Case",
                table: "MedicineCase",
                column: "CaseId",
                principalTable: "Case",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineCase_Medicine",
                table: "MedicineCase",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineIngredient_Ingredient",
                table: "MedicineIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineIngredient_Medicine",
                table: "MedicineIngredient",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_Patient",
                table: "Case");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Category",
                table: "Medicine");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineCase_Case",
                table: "MedicineCase");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineCase_Medicine",
                table: "MedicineCase");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineIngredient_Ingredient",
                table: "MedicineIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineIngredient_Medicine",
                table: "MedicineIngredient");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_Patient",
                table: "Case",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Category",
                table: "Medicine",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineCase_Case",
                table: "MedicineCase",
                column: "CaseId",
                principalTable: "Case",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineCase_Medicine",
                table: "MedicineCase",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineIngredient_Ingredient",
                table: "MedicineIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineIngredient_Medicine",
                table: "MedicineIngredient",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "Id");
        }
    }
}
