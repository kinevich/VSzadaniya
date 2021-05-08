using LibraryManagement.Data.Configurations;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class LibraryManagementContext : DbContext
    {
        public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new EditionConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryConfiguration());
            modelBuilder.ApplyConfiguration(new ReleaseFormConfiguration());            
        }

        public DbSet<Author> Author { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<District> District { get; set; }

        public DbSet<Edition> Edition { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<Library> Library { get; set; }

        public DbSet<ReleaseForm> ReleaseForm { get; set; }

        public DbSet<BookLibrary> BookLibrary { get; set; }
    }
}
