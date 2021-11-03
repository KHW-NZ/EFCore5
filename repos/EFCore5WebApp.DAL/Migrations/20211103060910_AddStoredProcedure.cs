using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore5WebApp.DAL.Migrations
{
    public partial class AddStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonPerson",
                newName: "PersonPerson",
                newSchema: "dbo");

            StringBuilder procBuilder1 = new StringBuilder();
            procBuilder1.Append(@"IF OBJECT_ID('GetPersonByState', 'P') IS NOT NULL");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("DROP PROC UpdateProfileCountry");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("GO");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("CREATE PROCEDURE [dbo].[GetPersonByState]");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("@State varchar(255)");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("AS");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("BEGIN");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("SELECT p.*");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("FROM Persons p");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("INNER JOIN Addresses a on p.Id = a.PersonId");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("WHERE a.State = @State");
            procBuilder1.Append(System.Environment.NewLine);
            procBuilder1.Append("END");

            StringBuilder procBuilder2 = new StringBuilder();
            procBuilder2.Append(@"IF OBJECT_ID('AddLookUpItem', 'P') IS NOT NULL");
            procBuilder2.Append(System.Environment.NewLine);
            procBuilder2.Append("DROP PROC AddLookUpItem");
            procBuilder2.Append(System.Environment.NewLine);
            procBuilder2.Append("GO");
            procBuilder2.Append(System.Environment.NewLine);
            procBuilder2.Append("CREATE PROCEDURE [dbo].[AddLookUpItem]");
            procBuilder2.Append("@Code varchar(255),");
            procBuilder2.Append(System.Environment.NewLine);
            procBuilder2.Append("@Description varchar(255),");
            procBuilder2.Append(System.Environment.NewLine);
            procBuilder2.Append("@LookUpTypeId int");
            procBuilder2.Append(System.Environment.NewLine);
            procBuilder2.Append("AS");
            procBuilder2.Append(System.Environment.NewLine);
            procBuilder2.Append("BEGIN");
            procBuilder2.Append(System.Environment.NewLine);
            procBuilder2.Append("INSERT INTO LookUps (Code, Description, LookUpType) VALUES (@Code, @Description, @LookUpTypeId)");
            procBuilder2.Append(System.Environment.NewLine);
            procBuilder2.Append("END");

            migrationBuilder.Sql(procBuilder1.ToString());
            migrationBuilder.Sql(procBuilder2.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonPerson",
                schema: "dbo",
                newName: "PersonPerson");

            migrationBuilder.Sql(@"DROP PROC GetPersonByState");
            migrationBuilder.Sql(@"DROP PROC AddLookUpItem");
        }
    }
}
