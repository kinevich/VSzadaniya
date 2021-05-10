using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202105080756)]
    public class AddBookLibrary : Migration
    {
        public override void Up()
        {
            Create.Table("BookLibrary")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("BookId").AsGuid()
                .WithColumn("LibraryId").AsGuid();                

            Create.ForeignKey()
                .FromTable("BookLibrary").ForeignColumn("BookId")
                .ToTable("Book").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.None);

            Create.ForeignKey()
                .FromTable("BookLibrary").ForeignColumn("LibraryId")
                .ToTable("Library").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.None);
        }

        public override void Down()
        {
            Delete.Table("BookLibrary");
        }
    }
}
