using YogaManagement_ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace YogaManagement_WebApp
{
    public class YogaDbContext: DbContext
    {
        public YogaDbContext(DbContextOptions<YogaDbContext> options)
            : base(options) { }

        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Class> Classs => Set<Class>();
        public DbSet<ClassLocation> ClassLocations => Set<ClassLocation>();
        public DbSet<InstructorClass> InstructorClasses => Set<InstructorClass>();

        // Define relationships and composite keys
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite primary key for InstructorClass
            modelBuilder.Entity<InstructorClass>()
                .HasKey(ic => new { ic.InstructorId, ic.ClassId });

            // Configure relationships for InstructorClass
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
