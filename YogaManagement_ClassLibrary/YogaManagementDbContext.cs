using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YogaManagement_ClassLibrary
{
    public class YogaManagementDbContext : DbContext
    {
        public YogaManagementDbContext() { }
        public YogaManagementDbContext(DbContextOptions<YogaManagementDbContext>options)
        : base(options) { }

        //Dbset properties
        public DbSet<YogaClass> Classes => Set<YogaClass>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<ClassLocation> ClassLocations => Set<ClassLocation>();
        public DbSet<InstructorClass> InstructorClasses => Set<InstructorClass>();

        // Configure the database connection
        //C:\Users\Roisi\OneDrive - Atlantic TU\5sem\richAppDev\week9\YogaManagementApp\YogaManagement_ClassLibrary\YogaManagement_ClassLibrary.csproj
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite(@"Data Source=C:\Users\Roisi\OneDrive - Atlantic TU\5sem\richAppDev\week9\YogaManagementApp\YogaManagement_ClassLibrary\yogamanagement.db");
            }
        }

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
            modelBuilder.Entity<YogaClass>()
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
