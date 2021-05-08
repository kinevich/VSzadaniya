using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Configurations
{
    public class BookLibraryConfiguration : IEntityTypeConfiguration<BookLibrary>
    {
        public void Configure(EntityTypeBuilder<BookLibrary> builder)
        {

        }
    }
}
