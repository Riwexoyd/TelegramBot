using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AspNetCoreTelegramBot.Migrations
{
    public partial class RouletteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteKeyword_Keywords_KeywordId",
                table: "QuoteKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteKeyword_Quotes_QuoteId",
                table: "QuoteKeyword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteKeyword",
                table: "QuoteKeyword");

            migrationBuilder.RenameTable(
                name: "QuoteKeyword",
                newName: "QuoteKeywords");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteKeyword_KeywordId",
                table: "QuoteKeywords",
                newName: "IX_QuoteKeywords_KeywordId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Users",
                nullable: false,
                defaultValueSql: "now() at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Quotes",
                nullable: false,
                defaultValueSql: "now() at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Keywords",
                nullable: false,
                defaultValueSql: "now() at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Chats",
                nullable: false,
                defaultValueSql: "now() at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteKeywords",
                table: "QuoteKeywords",
                columns: new[] { "QuoteId", "KeywordId" });

            migrationBuilder.CreateTable(
                name: "RouletteCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    AuthorId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now() at time zone 'utc'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouletteCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouletteCategories_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserChats",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ChatId = table.Column<int>(nullable: false),
                    IsRouletteParticipant = table.Column<bool>(nullable: false),
                    RouletteJoinDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => new { x.UserId, x.ChatId });
                    table.ForeignKey(
                        name: "FK_UserChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouletteCategoryChats",
                columns: table => new
                {
                    ChatId = table.Column<int>(nullable: false),
                    RouletteCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouletteCategoryChats", x => new { x.ChatId, x.RouletteCategoryId });
                    table.ForeignKey(
                        name: "FK_RouletteCategoryChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouletteCategoryChats_RouletteCategories_RouletteCategoryId",
                        column: x => x.RouletteCategoryId,
                        principalTable: "RouletteCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouletteWinners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    UserId = table.Column<int>(nullable: true),
                    RouletteCategoryChatChatId = table.Column<int>(nullable: true),
                    RouletteCategoryChatRouletteCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouletteWinners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouletteWinners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouletteWinners_RouletteCategoryChats_RouletteCategoryChatC~",
                        columns: x => new { x.RouletteCategoryChatChatId, x.RouletteCategoryChatRouletteCategoryId },
                        principalTable: "RouletteCategoryChats",
                        principalColumns: new[] { "ChatId", "RouletteCategoryId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "RouletteCategories",
                columns: new[] { "Id", "AuthorId", "IsPublic", "Title" },
                values: new object[] { 1, null, true, "Красавчик дня" });

            migrationBuilder.CreateIndex(
                name: "IX_RouletteCategories_AuthorId",
                table: "RouletteCategories",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RouletteCategoryChats_RouletteCategoryId",
                table: "RouletteCategoryChats",
                column: "RouletteCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RouletteWinners_Date",
                table: "RouletteWinners",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_RouletteWinners_UserId",
                table: "RouletteWinners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RouletteWinners_RouletteCategoryChatChatId_RouletteCategory~",
                table: "RouletteWinners",
                columns: new[] { "RouletteCategoryChatChatId", "RouletteCategoryChatRouletteCategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_ChatId",
                table: "UserChats",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteKeywords_Keywords_KeywordId",
                table: "QuoteKeywords",
                column: "KeywordId",
                principalTable: "Keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteKeywords_Quotes_QuoteId",
                table: "QuoteKeywords",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteKeywords_Keywords_KeywordId",
                table: "QuoteKeywords");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteKeywords_Quotes_QuoteId",
                table: "QuoteKeywords");

            migrationBuilder.DropTable(
                name: "RouletteWinners");

            migrationBuilder.DropTable(
                name: "UserChats");

            migrationBuilder.DropTable(
                name: "RouletteCategoryChats");

            migrationBuilder.DropTable(
                name: "RouletteCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteKeywords",
                table: "QuoteKeywords");

            migrationBuilder.RenameTable(
                name: "QuoteKeywords",
                newName: "QuoteKeyword");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteKeywords_KeywordId",
                table: "QuoteKeyword",
                newName: "IX_QuoteKeyword_KeywordId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "now() at time zone 'utc'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Quotes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "now() at time zone 'utc'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Keywords",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "now() at time zone 'utc'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Chats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "now() at time zone 'utc'");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteKeyword",
                table: "QuoteKeyword",
                columns: new[] { "QuoteId", "KeywordId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteKeyword_Keywords_KeywordId",
                table: "QuoteKeyword",
                column: "KeywordId",
                principalTable: "Keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteKeyword_Quotes_QuoteId",
                table: "QuoteKeyword",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
