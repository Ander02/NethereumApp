using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NethereumApp.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EthereumContractInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Abi = table.Column<string>(nullable: true),
                    ByteCode = table.Column<string>(nullable: true),
                    ContractAddress = table.Column<string>(nullable: true),
                    TransactionHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EthereumContractInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EthereumContractInfo");
        }
    }
}
