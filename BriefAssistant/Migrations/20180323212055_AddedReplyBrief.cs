using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BriefAssistant.Migrations
{
    public partial class AddedReplyBrief : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Briefs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Initials",
                columns: table => new
                {
                    BriefId = table.Column<Guid>(nullable: false),
                    AppendixDocuments = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    CaseFactsStatement = table.Column<string>(nullable: true),
                    IssuesPresented = table.Column<string>(nullable: true),
                    OralArgumentStatement = table.Column<string>(nullable: true),
                    PublicationStatement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Initials", x => x.BriefId);
                    table.ForeignKey(
                        name: "FK_Initials_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Initials_Briefs_BriefId",
                        column: x => x.BriefId,
                        principalTable: "Briefs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    BriefId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.BriefId);
                    table.ForeignKey(
                        name: "FK_Replies_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Replies_Briefs_BriefId",
                        column: x => x.BriefId,
                        principalTable: "Briefs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Initials_ApplicationUserId",
                table: "Initials",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ApplicationUserId",
                table: "Replies",
                column: "ApplicationUserId");

            migrationBuilder.Sql(
                @"INSERT INTO ""Initials"" (""BriefId"", ""AppendixDocuments"", ""ApplicationUserId"", ""CaseFactsStatement"", ""IssuesPresented"", ""OralArgumentStatement"", ""PublicationStatement"") 
                  SELECT ""Id"", ""AppendixDocuments"", ""ApplicationUserId"", ""CaseFactsStatement"", ""IssuesPresented"", ""OralArgumentStatement"", ""PublicationStatement""
                  FROM ""Briefs""");

            migrationBuilder.DropColumn(
                name: "AppendixDocuments",
                table: "Briefs");

            migrationBuilder.DropColumn(
                name: "CaseFactsStatement",
                table: "Briefs");

            migrationBuilder.DropColumn(
                name: "IssuesPresented",
                table: "Briefs");

            migrationBuilder.DropColumn(
                name: "OralArgumentStatement",
                table: "Briefs");

            migrationBuilder.DropColumn(
                name: "PublicationStatement",
                table: "Briefs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Initials");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Briefs");

            migrationBuilder.AddColumn<string>(
                name: "AppendixDocuments",
                table: "Briefs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaseFactsStatement",
                table: "Briefs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuesPresented",
                table: "Briefs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OralArgumentStatement",
                table: "Briefs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicationStatement",
                table: "Briefs",
                nullable: true);
        }
    }
}
