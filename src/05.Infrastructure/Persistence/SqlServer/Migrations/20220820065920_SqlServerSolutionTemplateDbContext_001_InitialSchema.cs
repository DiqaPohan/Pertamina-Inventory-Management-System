using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer.Migrations
{
    public partial class SqlServerSolutionTemplateDbContext_001_InitialSchema : Migration
    {
        String strschema = "SolutionTemplate";
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.EnsureSchema(
                name: strschema);
            migrationBuilder.CreateTable(
               name: "Data",
               schema: strschema,
               columns: table => new
               {
                   Id = table.Column<long>(type: "bigint", nullable: false),
                   Code_Apps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Diagram_Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Diagram_Physical = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Capability_Level_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Capability_Level_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Bisnis_Process = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Utilization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_License = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_Ats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_Package = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   User_Management_Integration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   User_Manual_Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Business_Owner_Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Business_Owner_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Business_Owner_KBO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Business_Owner_Jabatan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Business_Owner_PIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Business_Owner_PIC_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Developer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Business_Analyst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Link_Application = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Users = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Start_Development = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Start_Implementation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Criticality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Service_Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Db_Server_Dev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Db_Name_Dev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   App_Server_Dev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Db_Server_Prod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Db_Name_Prod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   App_Server_Prod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                   CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                   ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Data", x => x.Id);
               });

            migrationBuilder.CreateTable(
               name: "ApplicationArea",
               schema: strschema,
               columns: table => new
               {
                   Id = table.Column<long>(type: "bigint", nullable: false),
                   Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                   CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                   ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_ApplicationArea", x => x.Id);
               });

            migrationBuilder.CreateTable(
                         name: "ApplicationCapabilityLevel1",
                         schema: strschema,
                         columns: table => new
                         {
                             Id = table.Column<long>(type: "bigint", nullable: false),
                             Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                             CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                             Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                             ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                             IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
                         },
                         constraints: table =>
                         {
                             table.PrimaryKey("PK_ApplicationCapabilityLevel1", x => x.Id);
                         });

            migrationBuilder.CreateTable(
                         name: "ApplicationCapabilityLevel2",
                         schema: strschema,
                         columns: table => new
                         {
                             Id = table.Column<long>(type: "bigint", nullable: false),
                             Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                             CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                             Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                             ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                             IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
                         },
                         constraints: table =>
                         {
                             table.PrimaryKey("PK_ApplicationCapabilityLevel2", x => x.Id);
                         });

            migrationBuilder.CreateTable(
                         name: "ApplicationCriticality",
                         schema: strschema,
                         columns: table => new
                         {
                             Id = table.Column<long>(type: "bigint", nullable: false),
                             Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                             Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                             CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                             Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                             ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                             IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
                         },
                         constraints: table =>
                         {
                             table.PrimaryKey("PK_ApplicationCriticality", x => x.Id);
                         });

            migrationBuilder.CreateTable(
                        name: "ApplicationLicense",
                        schema: strschema,
                        columns: table => new
                        {
                            Id = table.Column<long>(type: "bigint", nullable: false),
                            Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                            CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                            Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                            ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                            IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
                        },
                        constraints: table =>
                        {
                            table.PrimaryKey("PK_ApplicationLicense", x => x.Id);
                        });

            migrationBuilder.CreateTable(
                       name: "ApplicationPackage",
                       schema: strschema,
                       columns: table => new
                       {
                           Id = table.Column<long>(type: "bigint", nullable: false),
                           Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                           Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                           Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                           Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                           Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                           CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                           Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                           ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                           IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
                       },
                       constraints: table =>
                       {
                           table.PrimaryKey("PK_ApplicationPackage", x => x.Id);
                       });

            migrationBuilder.CreateTable(
                       name: "ApplicationStatus",
                       schema: strschema,
                       columns: table => new
                       {
                           Id = table.Column<long>(type: "bigint", nullable: false),
                           Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                           Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                           Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                           Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                           Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                           CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                           Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                           ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                           IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
                       },
                       constraints: table =>
                       {
                           table.PrimaryKey("PK_ApplicationStatus", x => x.Id);
                       });

            migrationBuilder.CreateTable(
                      name: "ApplicationType",
                      schema: strschema,
                      columns: table => new
                      {
                          Id = table.Column<long>(type: "bigint", nullable: false),
                          Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                          Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                          Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                          Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                          Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                          CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                          Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                          ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                          IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
                      },
                      constraints: table =>
                      {
                          table.PrimaryKey("PK_ApplicationType", x => x.Id);
                      });

            migrationBuilder.CreateTable(
                   name: "ApplicationUserManagement",
                   schema: strschema,
                   columns: table => new
                   {
                       Id = table.Column<long>(type: "bigint", nullable: false),
                       Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                       Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                       Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                       Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                       Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                       CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                       Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                       ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                       IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   },
                   constraints: table =>
                   {
                       table.PrimaryKey("PK_ApplicationUserManagement", x => x.Id);
                   });

            migrationBuilder.CreateTable(
                  name: "ApplicationUtilization",
                  schema: strschema,
                  columns: table => new
                  {
                      Id = table.Column<long>(type: "bigint", nullable: false),
                      Application_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                      Application_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                      Application_Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                      Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                      Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                      CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                      Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                      ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false),
                      IsDeleted = table.Column<string>(type: "nvarchar(100)", nullable: false),
                  },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_ApplicationUtilization", x => x.Id);
                  });
            migrationBuilder.CreateTable(
               name: "DraftHistoricalApplicationStatusPhase",
               schema: "SolutionTemplate",
               columns: table => new
               {
                   id = table.Column<long>(type: "bigint", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Code_Apps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                   Year = table.Column<int>(type: "int", nullable: true),
                   Month = table.Column<int>(type: "int", nullable: true),
                   Day = table.Column<int>(type: "int", nullable: true),
                   Phase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Approved_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   IsApproved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                   CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                   ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   IsDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_DraftHistoricalApplicationStatusPhase", x => x.id);
               });

            migrationBuilder.CreateTable(
                            name: "HistoricalApplicationStatusPhase",
                            schema: "SolutionTemplate",
                            columns: table => new
                            {
                                id = table.Column<long>(type: "bigint", nullable: false)
                                    .Annotation("SqlServer:Identity", "1, 1"),
                                Code_Apps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                                Year = table.Column<int>(type: "int", nullable: true),
                                Month = table.Column<int>(type: "int", nullable: true),
                                Day = table.Column<int>(type: "int", nullable: true),
                                Phase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                                ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                IsDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                            },
                            constraints: table =>
                            {
                                table.PrimaryKey("PK_HistoricalApplicationStatusPhase", x => x.id);
                            });

            migrationBuilder.CreateTable(
                           name: "RequestData",
                           schema: "SolutionTemplate",
                           columns: table => new
                           {
                               id = table.Column<long>(type: "bigint", nullable: false)
                                   .Annotation("SqlServer:Identity", "1, 1"),
                               Tipe_Request = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Code_Apps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Code_Apps_Update = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Application_Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Application_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Application_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Application_Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Diagram_Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Diagram_Physical = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Capability_Level_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Capability_Level_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Bisnis_Process = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Utilization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Application_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Application_License = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Application_Ats = table.Column<DateTime>(type: "datetime", nullable: true),
                               Application_Package = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               User_Management_Integration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               User_Manual_Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Business_Owner_Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Business_Owner_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Business_Owner_KBO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Business_Owner_Jabatan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Business_Owner_PIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Business_Owner_PIC_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Developer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Business_Analyst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Link_Application = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Users = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Start_Development = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Start_Implementation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Start_Rationalization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Criticality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Service_Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Db_Server_Dev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Db_Name_Dev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               App_Server_Dev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Db_Server_Prod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Db_Name_Prod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               App_Server_Prod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               IsApproved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Approved_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Company_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                               CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                               ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               IsDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                           },
                           constraints: table =>
                           {
                               table.PrimaryKey("PK_RequestData", x => x.id);
                           });



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Data",
               schema: strschema);

        }
    }
}
