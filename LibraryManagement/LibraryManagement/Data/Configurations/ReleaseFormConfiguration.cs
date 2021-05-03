using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Configurations
{
    public class ReleaseFormConfiguration : IEntityTypeConfiguration<ReleaseForm>
    {
        public void Configure(EntityTypeBuilder<ReleaseForm> builder)
        {
            builder.Property(i => i.Name)
                .IsRequired();
        }
    }
}
