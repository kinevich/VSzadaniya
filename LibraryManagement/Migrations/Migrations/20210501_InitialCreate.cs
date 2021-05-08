using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Migrations
{
    [Migration(202105010938)]
    public class InitialCreate : Migration
    {
        public override void Up()
        {
            Create.Table("District")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("Author")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("Genre")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("Book")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("GenreId").AsGuid()
                .WithColumn("AuthorId").AsGuid();

            Create.ForeignKey()
                .FromTable("Book").ForeignColumn("GenreId")
                .ToTable("Genre").PrimaryColumn("Id");

            Create.ForeignKey()
                .FromTable("Book").ForeignColumn("AuthorId")
                .ToTable("Author").PrimaryColumn("Id");

            Create.Table("Edition")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("EditionNumber").AsInt32().NotNullable()
                .WithColumn("PagesAmount").AsInt32().NotNullable()
                .WithColumn("BookId").AsGuid();

            Create.ForeignKey()
                .FromTable("Edition").ForeignColumn("BookId")
                .ToTable("Book").PrimaryColumn("Id");

            Create.Table("ReleaseForm")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("Library")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("LimitDays").AsInt32().NotNullable()
                .WithColumn("DistrictId").AsGuid(); 

            Create.ForeignKey()
                .FromTable("Library").ForeignColumn("DistrictId")
                .ToTable("District").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("District");
            Delete.Table("Author");
            Delete.Table("Genre");
            Delete.Table("Book");
            Delete.Table("Edition");
            Delete.Table("ReleaseForm");
            Delete.Table("Library");
        }
    }
}
