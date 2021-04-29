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

        public DbSet<Author> Author { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<BookCopy> BookCopy { get; set; }

        public DbSet<BookCopyLibrary> BookCopyLibrary { get; set; }

        public DbSet<District> District { get; set; }

        public DbSet<Edition> Edition { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<Library> Library { get; set; }

        public DbSet<ReleaseForm> ReleaseForm { get; set; }
    }
}
