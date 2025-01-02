using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YogaManagement_ClassLibrary;

namespace YogaManagement_WebApp.Data
{
    public class ApplicationDbContext : 
        IdentityDbContext<Instructor>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Class> Classs => Set<Class>();
        public DbSet<ClassLocation> ClassLocations => Set<ClassLocation>();
        public DbSet<InstructorClass> InstructorClasses => Set<InstructorClass>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the many-to-many relationship for InstructorClass
            modelBuilder.Entity<InstructorClass>()
                .HasKey(ic => new { ic.InstructorId, ic.ClassId });

            modelBuilder.Entity<InstructorClass>()
                .HasOne(ic => ic.FK_Instructor)
                .WithMany(i => i.InstructorClasses)
                .HasForeignKey(ic => ic.InstructorId);

            modelBuilder.Entity<InstructorClass>()
                .HasOne(ic => ic.FK_Class)
                .WithMany(c => c.InstructorClasses)
                .HasForeignKey(ic => ic.ClassId);

            // Configure relationships for Class and ClassLocation
            modelBuilder.Entity<Class>()
                .HasOne(c => c.ClassLocation)
                .WithMany(cl => cl.Classes)
                .HasForeignKey("ClassLocationId");

            // Convert enum lists to comma-separated strings
            modelBuilder.Entity<ClassLocation>()
                .Property(cl => cl.FormatsAvailable)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(e => Enum.Parse<ClassFormat>(e)).ToList()
                );

            modelBuilder.Entity<Instructor>()
                .Property(i => i.Specialities)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(e => Enum.Parse<YogaSpeciality>(e)).ToList()
                );

        }
    }
}

