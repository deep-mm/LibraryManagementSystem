using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.DataAccessLayer.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bookLibraryAssociations",
                columns: table => new
                {
                    bookLibraryAssociationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bookId = table.Column<int>(nullable: false),
                    libraryId = table.Column<int>(nullable: false),
                    isAvailable = table.Column<bool>(nullable: false),
                    isCheckedOut = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookLibraryAssociations", x => x.bookLibraryAssociationId);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    bookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(maxLength: 200, nullable: false),
                    author = table.Column<string>(maxLength: 100, nullable: false),
                    price = table.Column<double>(nullable: false),
                    genre = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.bookId);
                });

            migrationBuilder.CreateTable(
                name: "libraries",
                columns: table => new
                {
                    libraryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    locationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libraries", x => x.libraryId);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    locationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    locationName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.locationId);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    roleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "userBookAssociations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(nullable: false),
                    BookLibraryAssociationId = table.Column<int>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userBookAssociations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userName = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    roleId = table.Column<int>(nullable: false),
                    locationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookLibraryAssociations");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "libraries");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "userBookAssociations");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
