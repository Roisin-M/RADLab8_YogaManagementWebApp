using Microsoft.EntityFrameworkCore;
using YogaManagement_ClassLibrary;
namespace YogaManagement_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set up the DbContext with options
            var options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<YogaManagementDbContext>()
                .UseSqlite(@"Data Source=C:\Users\Roisi\OneDrive - Atlantic TU\5sem\richAppDev\week9\YogaManagementApp\YogaManagement_ClassLibrary\yogamanagement.db")
                .Options;

            using var context = new YogaManagementDbContext(options);

            // Seed the database (if not already seeded)
            if (!context.Classes.Any())
            {
                SeedDatabase(context);
            }

            Console.WriteLine("Executing LINQ Queries:\n");

            // Query 1: List all classes and their full details
            var allClasses = context.Classes
                .Include(c => c.ClassLocation)
                .Include(c => c.InstructorClasses)
                    .ThenInclude(ic => ic.FK_Instructor)
                .ToList();

            Console.WriteLine("1. All Classes with Full Details:");
            foreach (var cls in allClasses)
            {
                Console.WriteLine($"\nClass: {cls.Description}");
                Console.WriteLine($"Start: {cls.ClassStart}, End: {cls.ClassEnd}");
                Console.WriteLine($"Location: {cls.ClassLocation.Location}, Capacity: {cls.ClassLocation.MaxCapacity}");
                Console.WriteLine($"Specialities: {string.Join(", ", cls.Specialities)}");
                Console.WriteLine($"Formats: {string.Join(", ", cls.AvailableFormats)}");

                Console.WriteLine("Instructors:");
                foreach (var instructorClass in cls.InstructorClasses)
                {
                    Console.WriteLine($"\tInstructor: {instructorClass.FK_Instructor.Name}, Email: {instructorClass.FK_Instructor.Email}");
                }
            }

            // Query 2: List all instructors and their yoga specialities
            var allInstructors = context.Instructors
                .Include(i => i.InstructorClasses)
                    .ThenInclude(ic => ic.FK_Class)
                .ToList();

            Console.WriteLine("\n2. Instructors and their Yoga Specialities:");
            foreach (var instructor in allInstructors)
            {
                Console.WriteLine($"\nInstructor: {instructor.Name}, Email: {instructor.Email}");
                Console.WriteLine($"Specialities: {string.Join(", ", instructor.Specialities)}");

                Console.WriteLine("Classes:");
                foreach (var ic in instructor.InstructorClasses)
                {
                    Console.WriteLine($"\tClass: {ic.FK_Class.Description}, Start: {ic.FK_Class.ClassStart}");
                }
            }

            // Query 3: Classes with a specific format (e.g., Stream)
            var streamClasses = context.Classes
                .Where(c => c.AvailableFormats.Contains(ClassFormat.Stream))
                .ToList();

            Console.WriteLine("\n3. Classes with 'Stream' Format:");
            foreach (var cls in streamClasses)
            {
                Console.WriteLine($"Class: {cls.Description}, Location: {cls.ClassLocation.Location}");
            }

            // Query 4: Instructors teaching more than one class
            var busyInstructors = context.Instructors
                .Where(i => i.InstructorClasses.Count > 1)
                .ToList();

            Console.WriteLine("\n4. Instructors Teaching More Than One Class:");
            foreach (var instructor in busyInstructors)
            {
                Console.WriteLine($"Instructor: {instructor.Name}, Classes: {instructor.InstructorClasses.Count}");
            }

            // Query 5: List class locations and the number of classes hosted
            var locationClassCounts = context.ClassLocations
                .Select(cl => new
                {
                    Location = cl.Location,
                    ClassCount = cl.Classes.Count
                })
                .ToList();

            Console.WriteLine("\n5. Class Locations and Number of Classes Hosted:");
            foreach (var location in locationClassCounts)
            {
                Console.WriteLine($"Location: {location.Location}, Classes Hosted: {location.ClassCount}");
            }
        }
        private static void SeedDatabase(YogaManagementDbContext context)
        {
            Console.WriteLine("Seeding database...");

            // Class Locations
            var location1 = new ClassLocation
            {
                description = "Main Studio",
                Location = "City Center",
                MaxCapacity = 25,
                FormatsAvailable = new List<ClassFormat> { ClassFormat.OnLocation, ClassFormat.Hybrid }
            };

            var location2 = new ClassLocation
            {
                description = "Downtown Studio",
                Location = "Downtown",
                MaxCapacity = 15,
                FormatsAvailable = new List<ClassFormat> { ClassFormat.Stream, ClassFormat.OnLocation }
            };

            context.ClassLocations.AddRange(location1, location2);
            context.SaveChanges(); // Ensure these are saved first

            // Instructors
            var instructor1 = new Instructor
            {
                Name = "Alice Smith",
                Email = "alice@yogastudio.com",
                Specialities = new List<YogaSpeciality> { YogaSpeciality.Hatha, YogaSpeciality.Ashtanga }
            };

            var instructor2 = new Instructor
            {
                Name = "Bob Johnson",
                Email = "bob@yogastudio.com",
                Specialities = new List<YogaSpeciality> { YogaSpeciality.Vinyasa, YogaSpeciality.Yin }
            };

            context.Instructors.AddRange(instructor1, instructor2);
            context.SaveChanges();

            // Classes
            var class1 = new Class
            {
                Description = "Morning Flow",
                ClassStart = DateTime.Now.AddHours(2),
                ClassEnd = DateTime.Now.AddHours(3),
                ClassLocation = location1,
                AvailableFormats = new List<ClassFormat> { ClassFormat.OnLocation },
                Specialities = new List<YogaSpeciality> { YogaSpeciality.Hatha, YogaSpeciality.Vinyasa }
            };

            var class2 = new Class
            {
                Description = "Evening Relaxation",
                ClassStart = DateTime.Now.AddHours(5),
                ClassEnd = DateTime.Now.AddHours(6),
                ClassLocation = location2,
                AvailableFormats = new List<ClassFormat> { ClassFormat.Stream },
                Specialities = new List<YogaSpeciality> { YogaSpeciality.Yin, YogaSpeciality.Restorative }
            };

            context.Classes.AddRange(class1, class2);
            context.SaveChanges();

            // Instructor-Class Relationships
            var instructorClass1 = new InstructorClass { FK_Instructor = instructor1, FK_Class = class1 };
            var instructorClass2 = new InstructorClass { FK_Instructor = instructor2, FK_Class = class2 };

            context.InstructorClasses.AddRange(instructorClass1, instructorClass2);
            context.SaveChanges();

            Console.WriteLine("Database seeded successfully!");

            Console.WriteLine("Seeding Class Locations...");
            context.ClassLocations.ToList().ForEach(l => Console.WriteLine($"Location: {l.Location}"));

            Console.WriteLine("Seeding Classes...");
            context.Classes.ToList().ForEach(c => Console.WriteLine($"Class: {c.Description}, Location: {c.ClassLocation?.Location}"));

            Console.WriteLine("Seeding Instructors...");
            context.Instructors.ToList().ForEach(i => Console.WriteLine($"Instructor: {i.Name}, Email: {i.Email}"));

        }
    }
}
