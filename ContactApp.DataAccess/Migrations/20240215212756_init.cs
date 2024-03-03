using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(name: "first_name", type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "nvarchar(max)", nullable: false),
                    workemail = table.Column<string>(name: "work_email", type: "nvarchar(max)", nullable: false),
                    personalemail = table.Column<string>(name: "personal_email", type: "nvarchar(max)", nullable: false),
                    company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jobtitle = table.Column<string>(name: "job_title", type: "nvarchar(max)", nullable: true),
                    joblevel = table.Column<string>(name: "job_level", type: "nvarchar(max)", nullable: true),
                    jobdepartment = table.Column<string>(name: "job_department", type: "nvarchar(max)", nullable: true),
                    workphone = table.Column<string>(name: "work_phone", type: "nvarchar(max)", nullable: true),
                    workmobile = table.Column<string>(name: "work_mobile", type: "nvarchar(max)", nullable: true),
                    workdbn = table.Column<string>(name: "work_dbn", type: "nvarchar(max)", nullable: true),
                    mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    altmobile = table.Column<string>(name: "alt_mobile", type: "nvarchar(max)", nullable: true),
                    branchphone = table.Column<string>(name: "branch_phone", type: "nvarchar(max)", nullable: true),
                    hqphone = table.Column<string>(name: "hq_phone", type: "nvarchar(max)", nullable: true),
                    street1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locationtype = table.Column<string>(name: "location_type", type: "nvarchar(max)", nullable: true),
                    companyrevenue = table.Column<string>(name: "company_revenue", type: "nvarchar(max)", nullable: true),
                    companyemployeecount = table.Column<string>(name: "company_employee_count", type: "nvarchar(max)", nullable: true),
                    linkedinurl = table.Column<string>(name: "linkedin_url", type: "nvarchar(max)", nullable: true),
                    twitterurl = table.Column<string>(name: "twitter_url", type: "nvarchar(max)", nullable: true),
                    companylinkedinurl = table.Column<string>(name: "company_linkedin_url", type: "nvarchar(max)", nullable: true),
                    naicscode = table.Column<string>(name: "naics_code", type: "nvarchar(max)", nullable: true),
                    siccode = table.Column<string>(name: "sic_code", type: "nvarchar(max)", nullable: true),
                    companysector = table.Column<string>(name: "company_sector", type: "nvarchar(max)", nullable: true),
                    companyindustry = table.Column<string>(name: "company_industry", type: "nvarchar(max)", nullable: true),
                    keywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExportTodayLimit = table.Column<int>(type: "int", nullable: false),
                    PerExportLimit = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblContacts");

            migrationBuilder.DropTable(
                name: "tblUsers");
        }
    }
}
