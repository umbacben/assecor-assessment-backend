using Microsoft.EntityFrameworkCore;
using assecor_assessment_backend.Models;

namespace assecor_assessment_backend.Data
{
    public class PersonsContext : DbContext
    {
        public PersonsContext(DbContextOptions<PersonsContext> options)
            : base(options)
        {
        }
        public DbSet<Persons> Persons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persons>().ToTable("Persons");
        }
    }
}
