using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HRM.ERP.Migrations
{
    public partial class job : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Job",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Job_DepartmentId",
                table: "Job",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Departments_DepartmentId",
                table: "Job",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Departments_DepartmentId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_DepartmentId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Job");
        }
    }
}
