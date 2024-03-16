using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateExplorer.Migrations
{
    public partial class SeedRoles : Migration
    {
        private string AdministrativniRadnikRoleId = Guid.NewGuid().ToString();
        private string KupacRoleId = Guid.NewGuid().ToString();
        private string AdminRoleId = Guid.NewGuid().ToString();
        private string InvestitorRoleId = Guid.NewGuid().ToString();

        private string User1Id = Guid.NewGuid().ToString();
        private string User2Id = Guid.NewGuid().ToString();
        private string User3Id = Guid.NewGuid().ToString();

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);

            SeedUser(migrationBuilder);

            SeedUserRoles(migrationBuilder);
        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{AdminRoleId}', 'Administrator', 'ADMINISTRATOR', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{AdministrativniRadnikRoleId}', 'AdministrativniRadnik', 'ADMINISTRATIVNIRADNIK', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{KupacRoleId}', 'Kupac', 'KUPAC', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{InvestitorRoleId}', 'Investitor', 'INVESTITOR', null);");
        }

        private void SeedUser(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @$"INSERT [dbo].[AspNetUsers] ([Id], [Ime], [Prezime],[JMBG], [UserName], [NormalizedUserName], 
[Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], 
[PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
VALUES 
(N'{User1Id}', N'Stanko', N'Puflovic', NULL, N'stankopufke@gmail.com', N'STANKOPUFKE@GMAIL.COM', N'stankopufke@gmail.com',	N'STANKOPUFKE@GMAIL.COM',	
N'True', N'AQAAAAIAAYagAAAAEHqEKbrMoU5oR7pmz51bJal24EIG2I+mYhPTWd9PjxPj6PEXz4PyLFFTCIvZTgIG5Q==',	N'3YBSL23JUMGT7UG3HAV7HR6CDNRPQJ2E', 
N'2069eb2d-01e6-4b45-bd5f-0968d1dd693d', NULL,	N'False',	N'False',	NULL,	N'True',	0)");


            migrationBuilder.Sql(
                @$"INSERT [dbo].[AspNetUsers] ([Id], [Ime], [Prezime],[JMBG], [UserName], [NormalizedUserName], 
[Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], 
[PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
VALUES 
(N'{User2Id}',N'Tasi',	N'Vasic',	NULL, N'tasi@gmail.com',	N'TASI@GMAIL.COM',	N'tasi@gmail.com',	N'TASI@GMAIL.COM',	N'True',
N'AQAAAAIAAYagAAAAENWF9FG6yT4J+ap5HE8QIPaP8FlZiDgWruVOpTvsor36mE+iSSvCmbWPRB7yoEhe3g==',	N'VYRW3E2CY2JQHZZVEADEN7KMVMWCWKTY',	
N'e18810c5-35dc-4bbe-a6ae-0a37e0716ece',	NULL,	N'False',	N'False',	NULL,	N'True',	0)");

            migrationBuilder.Sql(
                @$"INSERT [dbo].[AspNetUsers] ([Id], [Ime], [Prezime],[JMBG], [UserName], [NormalizedUserName], 
[Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], 
[PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
VALUES 
(N'{User3Id}', 	NULL,	NULL,	NULL,	N'pufla@puflander.com',	N'PUFLA@PUFLANDER.COM',	N'pufla@puflander.com',	N'PUFLA@PUFLANDER.COM',	N'True',	
N'AQAAAAIAAYagAAAAEHLmS+Y5fKyVjFlT3+bZ1UPgN1ohvzxuZ9SIyxjdTkOMWE4x7xucxsbC/LKNLa3DnQ==',	N'TODMYOZP3OT4GK5DRFPBPNXF3KQK5DKN',	
N'8b2f57e1-2984-4b1f-a0dd-1dd5cc957e0b',	NULL,	N'False',	N'False',	NULL,	N'True',	0)");
        }
    


        private void SeedUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"
        INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
        VALUES
           ('{User1Id}', '{AdministrativniRadnikRoleId}');");     

            migrationBuilder.Sql(@$"
        INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
        VALUES
           ('{User2Id}', '{AdminRoleId}');");

            migrationBuilder.Sql(@$"
        INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
        VALUES
           ('{User3Id}', '{InvestitorRoleId}');");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
