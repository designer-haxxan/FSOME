using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class chnagetblcontact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alt_mobile",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "branch_phone",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "company_linkedin_url",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "hq_phone",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "keywords",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "location_type",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "sic_code",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "street2",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "twitter_url",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "work_dbn",
                table: "tblContacts");

            migrationBuilder.DropColumn(
                name: "work_mobile",
                table: "tblContacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "alt_mobile",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "branch_phone",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company_linkedin_url",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hq_phone",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keywords",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location_type",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sic_code",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "street2",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "twitter_url",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "work_dbn",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "work_mobile",
                table: "tblContacts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
