using LMS.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LMS.DataAccessLayer.DatabaseContext
{
    public class ReadDBContext: DbContext
    {
        public ReadDBContext(DbContextOptions<ReadDBContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Book> books { get; set; }

        public DbSet<BookLibraryAssociation> bookLibraryAssociations { get; set; }

        public DbSet<Library> libraries { get; set; }

        public DbSet<Location> locations { get; set; }

        public DbSet<Role> roles { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<UserBookAssociation> userBookAssociations { get; set; }
        public object ContextOptions { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelbuilder);
        }

    }
}
