using DotTree.Domain.Entities;
using System.Data.Entity;

namespace DotTree.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Family> Families { get; set; }
    }
}
