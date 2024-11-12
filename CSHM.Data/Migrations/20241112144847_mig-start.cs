using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSHM.Data.Migrations
{
    /// <inheritdoc />
    public partial class migstart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogStatusTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogStatusTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BlogTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TitleEN = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CalenderDimensions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    GregDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GregKey = table.Column<int>(type: "int", nullable: false),
                    JalaliDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HijriDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HijriKey = table.Column<int>(type: "int", nullable: false),
                    YearKey = table.Column<int>(type: "int", nullable: false),
                    YearName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeasonKey = table.Column<int>(type: "int", nullable: false),
                    SeasonName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MonthKey = table.Column<int>(type: "int", nullable: false),
                    MonthName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WeekOfYearKey = table.Column<int>(type: "int", nullable: false),
                    DayOfWeekKey = table.Column<int>(type: "int", nullable: false),
                    DayOfWeekName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DayKey = table.Column<int>(type: "int", nullable: false),
                    DayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DayOfYearKey = table.Column<int>(type: "int", nullable: false),
                    JalaliAlphabetic = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GregYearKey = table.Column<int>(type: "int", nullable: false),
                    GregSeasonKey = table.Column<int>(type: "int", nullable: false),
                    GregMonthKey = table.Column<int>(type: "int", nullable: false),
                    GregWeekKey = table.Column<int>(type: "int", nullable: false),
                    GregDayKey = table.Column<int>(type: "int", nullable: false),
                    GregSeasonName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GregSeasonNameFa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GregMonthName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GregMonthNameFa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SeasonIndex = table.Column<int>(type: "int", nullable: false),
                    MonthIndex = table.Column<int>(type: "int", nullable: false),
                    WeekIndex = table.Column<int>(type: "int", nullable: false),
                    DayIndex = table.Column<int>(type: "int", nullable: false),
                    WorkdayIndex = table.Column<int>(type: "int", nullable: false),
                    MidYear = table.Column<int>(type: "int", nullable: false),
                    MidSeason = table.Column<int>(type: "int", nullable: false),
                    MidMonth = table.Column<int>(type: "int", nullable: false),
                    IsHoliday = table.Column<bool>(type: "bit", nullable: false),
                    IsWorkDay = table.Column<bool>(type: "bit", nullable: false),
                    IsFirstOfYear = table.Column<bool>(type: "bit", nullable: false),
                    IsLastOfYear = table.Column<bool>(type: "bit", nullable: false),
                    IsFirstOfSeason = table.Column<bool>(type: "bit", nullable: false),
                    IsLastOfSeason = table.Column<bool>(type: "bit", nullable: false),
                    IsFirstOfMonth = table.Column<bool>(type: "bit", nullable: false),
                    IsLastOfMonth = table.Column<bool>(type: "bit", nullable: false),
                    IsFirstOfWeek = table.Column<bool>(type: "bit", nullable: false),
                    IsLastOfWeek = table.Column<bool>(type: "bit", nullable: false),
                    IsLeap = table.Column<bool>(type: "bit", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalenderDimensions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CoverTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExtensionTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Postfix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatcherType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Matcher = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsImage = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GenderType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GeoCountry",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoCountry", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GeoProvince",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoProvince", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IDNPages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsMenu = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNPages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IDNPages_IDNPages_ParentID",
                        column: x => x.ParentID,
                        principalTable: "IDNPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IDNPolicyParameters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Side = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsMultiple = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNPolicyParameters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IDNRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Side = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IDNUserTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNUserTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lables",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lables", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MediaTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Occupations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PersonType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cellphone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PublishTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SizeTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogTypeID = table.Column<int>(type: "int", nullable: false),
                    BlogStatusTypeID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreationTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    VisitedCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogStatusTypes_BlogStatusTypeID",
                        column: x => x.BlogStatusTypeID,
                        principalTable: "BlogStatusTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogTypes_BlogTypeID",
                        column: x => x.BlogTypeID,
                        principalTable: "BlogTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalenderEvents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    CalenderType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsHoliday = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    CalenderDimensionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalenderEvents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalenderEvents_CalenderDimensions_CalenderDimensionID",
                        column: x => x.CalenderDimensionID,
                        principalTable: "CalenderDimensions",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderTypeID = table.Column<int>(type: "int", nullable: false),
                    GeoCountryID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AliasName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.ID);
                    table.ForeignKey(
                        name: "FK_People_GenderType_GenderTypeID",
                        column: x => x.GenderTypeID,
                        principalTable: "GenderType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_GeoCountry_GeoCountryID",
                        column: x => x.GeoCountryID,
                        principalTable: "GeoCountry",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GeoCity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeoProvinceID = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoCity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GeoCity_GeoProvince_GeoProvinceID",
                        column: x => x.GeoProvinceID,
                        principalTable: "GeoProvince",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDNControllerActions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false),
                    TitleFa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNControllerActions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IDNControllerActions_IDNPages_PageID",
                        column: x => x.PageID,
                        principalTable: "IDNPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IDNPolicies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyParameterID = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNPolicies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IDNPolicies_IDNPolicyParameters_PolicyParameterID",
                        column: x => x.PolicyParameterID,
                        principalTable: "IDNPolicyParameters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IDNRoleClaims",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    ControllerActionCode = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNRoleClaims", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IDNRoleClaims_IDNRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "IDNRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherID = table.Column<int>(type: "int", nullable: false),
                    ProductTypeID = table.Column<int>(type: "int", nullable: false),
                    PublishTypeID = table.Column<int>(type: "int", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    SizeTypeID = table.Column<int>(type: "int", nullable: false),
                    CoverTypeID = table.Column<int>(type: "int", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    BeforePrice = table.Column<long>(type: "bigint", nullable: false),
                    StudyTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PublisheYear = table.Column<int>(type: "int", nullable: false),
                    PublishSeason = table.Column<int>(type: "int", nullable: false),
                    PublishTurn = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MetaDercreption = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    PageCount = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    VisitedCount = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_CoverTypes_CoverTypeID",
                        column: x => x.CoverTypeID,
                        principalTable: "CoverTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_PublishTypes_PublishTypeID",
                        column: x => x.PublishTypeID,
                        principalTable: "PublishTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_SizeTypes_SizeTypeID",
                        column: x => x.SizeTypeID,
                        principalTable: "SizeTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPublishers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogID = table.Column<int>(type: "int", nullable: false),
                    PublisherID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPublishers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogPublishers_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogPublishers_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonOccupations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    OccupationID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    PersonID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonOccupations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonOccupations_Occupations_OccupationID",
                        column: x => x.OccupationID,
                        principalTable: "Occupations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonOccupations_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonOccupations_People_PersonID1",
                        column: x => x.PersonID1,
                        principalTable: "People",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "IDNUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeID = table.Column<int>(type: "int", nullable: false),
                    PersonTypeID = table.Column<int>(type: "int", nullable: false),
                    GenderTypeID = table.Column<int>(type: "int", nullable: false),
                    GeoCityID = table.Column<int>(type: "int", nullable: false),
                    NID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AliasName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RegDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RegNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Cellphone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegistrationDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsGaurd = table.Column<bool>(type: "bit", nullable: false),
                    IsForced = table.Column<bool>(type: "bit", nullable: false),
                    HasTicketum = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IDNUsers_GenderType_GenderTypeID",
                        column: x => x.GenderTypeID,
                        principalTable: "GenderType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDNUsers_GeoCity_GeoCityID",
                        column: x => x.GeoCityID,
                        principalTable: "GeoCity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDNUsers_IDNUserTypes_UserTypeID",
                        column: x => x.UserTypeID,
                        principalTable: "IDNUserTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDNUsers_PersonType_PersonTypeID",
                        column: x => x.PersonTypeID,
                        principalTable: "PersonType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublisherBranches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherID = table.Column<int>(type: "int", nullable: false),
                    GeoCityID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Cellphone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherBranches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PublisherBranches_GeoCity_GeoCityID",
                        column: x => x.GeoCityID,
                        principalTable: "GeoCity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublisherBranches_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaTypeID = table.Column<int>(type: "int", nullable: false),
                    ExtensionTypeID = table.Column<int>(type: "int", nullable: false),
                    BlogID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EntityID = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ExtensionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Medias_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medias_ExtensionTypes_ExtensionTypeID",
                        column: x => x.ExtensionTypeID,
                        principalTable: "ExtensionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medias_MediaTypes_MediaTypeID",
                        column: x => x.MediaTypeID,
                        principalTable: "MediaTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medias_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategoryTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    CategoryTypeID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    ProductID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategoryTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductCategoryTypes_CategoryTypes_CategoryTypeID",
                        column: x => x.CategoryTypeID,
                        principalTable: "CategoryTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategoryTypes_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategoryTypes_Products_ProductID1",
                        column: x => x.ProductID1,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductLables",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    LableID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    ProductID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLables", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductLables_Lables_LableID",
                        column: x => x.LableID,
                        principalTable: "Lables",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductLables_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductLables_Products_ProductID1",
                        column: x => x.ProductID1,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductPublishers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    PublisherID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPublishers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductPublishers_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPublishers_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogOccupations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogID = table.Column<int>(type: "int", nullable: false),
                    PersonOccupationID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogOccupations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogOccupations_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogOccupations_PersonOccupations_PersonOccupationID",
                        column: x => x.PersonOccupationID,
                        principalTable: "PersonOccupations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductOccupations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    PersonOccupationID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    ProductID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOccupations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductOccupations_PersonOccupations_PersonOccupationID",
                        column: x => x.PersonOccupationID,
                        principalTable: "PersonOccupations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOccupations_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOccupations_Products_ProductID1",
                        column: x => x.ProductID1,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "IDNUserPolicies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNUserPolicies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IDNUserPolicies_IDNPolicies_PolicyID",
                        column: x => x.PolicyID,
                        principalTable: "IDNPolicies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDNUserPolicies_IDNUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "IDNUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IDNUsersInRoles",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpiryDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNUsersInRoles", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_IDNUsersInRoles_IDNRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "IDNRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDNUsersInRoles_IDNUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "IDNUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductComments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    NoteDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NoteTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ModifierID = table.Column<int>(type: "int", nullable: true),
                    ModificationDateTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    ProductID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductComments_IDNUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "IDNUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductID1",
                        column: x => x.ProductID1,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogOccupations_BlogID",
                table: "BlogOccupations",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogOccupations_PersonOccupationID",
                table: "BlogOccupations",
                column: "PersonOccupationID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPublishers_BlogID",
                table: "BlogPublishers",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPublishers_PublisherID",
                table: "BlogPublishers",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogStatusTypeID",
                table: "Blogs",
                column: "BlogStatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogTypeID",
                table: "Blogs",
                column: "BlogTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CalenderEvents_CalenderDimensionID",
                table: "CalenderEvents",
                column: "CalenderDimensionID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoCity_GeoProvinceID",
                table: "GeoCity",
                column: "GeoProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_IDNControllerActions_PageID",
                table: "IDNControllerActions",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_IDNPages_ParentID",
                table: "IDNPages",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_IDNPolicies_PolicyParameterID",
                table: "IDNPolicies",
                column: "PolicyParameterID");

            migrationBuilder.CreateIndex(
                name: "IX_IDNRoleClaims_RoleID",
                table: "IDNRoleClaims",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "IDNRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IDNUserPolicies_PolicyID",
                table: "IDNUserPolicies",
                column: "PolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_IDNUserPolicies_UserID",
                table: "IDNUserPolicies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_IDNUsers_GenderTypeID",
                table: "IDNUsers",
                column: "GenderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_IDNUsers_GeoCityID",
                table: "IDNUsers",
                column: "GeoCityID");

            migrationBuilder.CreateIndex(
                name: "IX_IDNUsers_PersonTypeID",
                table: "IDNUsers",
                column: "PersonTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_IDNUsers_UserTypeID",
                table: "IDNUsers",
                column: "UserTypeID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "IDNUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IDNUsersInRoles_RoleID",
                table: "IDNUsersInRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_BlogID",
                table: "Medias",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_ExtensionTypeID",
                table: "Medias",
                column: "ExtensionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_MediaTypeID",
                table: "Medias",
                column: "MediaTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_ProductID",
                table: "Medias",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_People_GenderTypeID",
                table: "People",
                column: "GenderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_People_GeoCountryID",
                table: "People",
                column: "GeoCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonOccupations_OccupationID",
                table: "PersonOccupations",
                column: "OccupationID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonOccupations_PersonID",
                table: "PersonOccupations",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonOccupations_PersonID1",
                table: "PersonOccupations",
                column: "PersonID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryTypes_CategoryTypeID",
                table: "ProductCategoryTypes",
                column: "CategoryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryTypes_ProductID",
                table: "ProductCategoryTypes",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryTypes_ProductID1",
                table: "ProductCategoryTypes",
                column: "ProductID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductID",
                table: "ProductComments",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductID1",
                table: "ProductComments",
                column: "ProductID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserID",
                table: "ProductComments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLables_LableID",
                table: "ProductLables",
                column: "LableID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLables_ProductID",
                table: "ProductLables",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLables_ProductID1",
                table: "ProductLables",
                column: "ProductID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOccupations_PersonOccupationID",
                table: "ProductOccupations",
                column: "PersonOccupationID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOccupations_ProductID",
                table: "ProductOccupations",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOccupations_ProductID1",
                table: "ProductOccupations",
                column: "ProductID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPublishers_ProductID",
                table: "ProductPublishers",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPublishers_PublisherID",
                table: "ProductPublishers",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CoverTypeID",
                table: "Products",
                column: "CoverTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LanguageID",
                table: "Products",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeID",
                table: "Products",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PublisherID",
                table: "Products",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PublishTypeID",
                table: "Products",
                column: "PublishTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeTypeID",
                table: "Products",
                column: "SizeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherBranches_GeoCityID",
                table: "PublisherBranches",
                column: "GeoCityID");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherBranches_PublisherID",
                table: "PublisherBranches",
                column: "PublisherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogOccupations");

            migrationBuilder.DropTable(
                name: "BlogPublishers");

            migrationBuilder.DropTable(
                name: "CalenderEvents");

            migrationBuilder.DropTable(
                name: "IDNControllerActions");

            migrationBuilder.DropTable(
                name: "IDNRoleClaims");

            migrationBuilder.DropTable(
                name: "IDNUserPolicies");

            migrationBuilder.DropTable(
                name: "IDNUsersInRoles");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "ProductCategoryTypes");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "ProductLables");

            migrationBuilder.DropTable(
                name: "ProductOccupations");

            migrationBuilder.DropTable(
                name: "ProductPublishers");

            migrationBuilder.DropTable(
                name: "PublisherBranches");

            migrationBuilder.DropTable(
                name: "CalenderDimensions");

            migrationBuilder.DropTable(
                name: "IDNPages");

            migrationBuilder.DropTable(
                name: "IDNPolicies");

            migrationBuilder.DropTable(
                name: "IDNRoles");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "ExtensionTypes");

            migrationBuilder.DropTable(
                name: "MediaTypes");

            migrationBuilder.DropTable(
                name: "CategoryTypes");

            migrationBuilder.DropTable(
                name: "IDNUsers");

            migrationBuilder.DropTable(
                name: "Lables");

            migrationBuilder.DropTable(
                name: "PersonOccupations");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "IDNPolicyParameters");

            migrationBuilder.DropTable(
                name: "BlogStatusTypes");

            migrationBuilder.DropTable(
                name: "BlogTypes");

            migrationBuilder.DropTable(
                name: "GeoCity");

            migrationBuilder.DropTable(
                name: "IDNUserTypes");

            migrationBuilder.DropTable(
                name: "PersonType");

            migrationBuilder.DropTable(
                name: "Occupations");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "CoverTypes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "PublishTypes");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "SizeTypes");

            migrationBuilder.DropTable(
                name: "GeoProvince");

            migrationBuilder.DropTable(
                name: "GenderType");

            migrationBuilder.DropTable(
                name: "GeoCountry");
        }
    }
}
