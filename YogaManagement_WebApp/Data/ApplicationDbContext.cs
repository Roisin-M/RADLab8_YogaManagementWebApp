using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
       // public DbSet<Instructor> Instructors => Set<Instructor>();

        public DbSet<Class> Classs => Set<Class>();
        public DbSet<ClassLocation> ClassLocations => Set<ClassLocation>();
        public DbSet<InstructorClass> InstructorClasses => Set<InstructorClass>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adjust column types for SQLite compatibility
            //modelBuilder.Entity<IdentityRole>(entity =>
            //{
            //    entity.Property(e => e.ConcurrencyStamp).HasColumnType("TEXT");
            //    entity.Property(e => e.Name).HasColumnType("TEXT").HasMaxLength(256);
            //    entity.Property(e => e.NormalizedName).HasColumnType("TEXT").HasMaxLength(256);
            //});

            //modelBuilder.Entity<Instructor>(entity =>
            //{
            //    entity.Property(e => e.ConcurrencyStamp).HasColumnType("TEXT");
            //    entity.Property(e => e.Email).HasColumnType("TEXT");
            //    entity.Property(e => e.NormalizedEmail).HasColumnType("TEXT");
            //    entity.Property(e => e.NormalizedUserName).HasColumnType("TEXT");
            //    entity.Property(e => e.UserName).HasColumnType("TEXT");
            //});

            //modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            //{
            //    entity.HasKey(key => new { key.UserId, key.RoleId });
            //});

            //modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            //{
            //    entity.Property(e => e.ClaimType).HasColumnType("TEXT");
            //    entity.Property(e => e.ClaimValue).HasColumnType("TEXT");
            //});

            //modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            //{
            //    entity.Property(e => e.ClaimType).HasColumnType("TEXT");
            //    entity.Property(e => e.ClaimValue).HasColumnType("TEXT");
            //});

            //modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            //{
            //    entity.Property(e => e.ProviderKey).HasColumnType("TEXT");
            //    entity.Property(e => e.LoginProvider).HasColumnType("TEXT");
            //});

            //modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            //{
            //    entity.Property(e => e.LoginProvider).HasColumnType("TEXT");
            //    entity.Property(e => e.Value).HasColumnType("TEXT");
            //    entity.Property(e => e.Name).HasColumnType("TEXT");
            //});
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

            // Add Value Comparer for FormatsAvailable
            var classFormatComparer = new ValueComparer<List<ClassFormat>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            );

            modelBuilder.Entity<ClassLocation>()
                .Property(cl => cl.FormatsAvailable)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(e => Enum.Parse<ClassFormat>(e)).ToList()
                )
                .Metadata.SetValueComparer(classFormatComparer);

            // Add Value Comparer for Specialities
            var yogaSpecialityComparer = new ValueComparer<List<YogaSpeciality>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            );

            modelBuilder.Entity<Instructor>()
                .Property(i => i.Specialities)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(e => Enum.Parse<YogaSpeciality>(e)).ToList()
                )
                .Metadata.SetValueComparer(yogaSpecialityComparer);
        }

    }
    
}

