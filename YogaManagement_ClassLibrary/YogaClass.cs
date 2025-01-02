namespace YogaManagement_ClassLibrary
{
    public class YogaClass
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ClassStart { get; set; }
        public DateTime ClassEnd { get; set; }
        // many - to one relationship with Class Location
        public ClassLocation ClassLocation { get; set; }

        //Many-to-many relationship with instructor via junction table
        public virtual ICollection<InstructorClass> InstructorClasses { get; set; }
        = new List<InstructorClass>();

        // Many-to-many relationship with ClassFormats
        public List<ClassFormat> AvailableFormats { get; set; }
            = new List<ClassFormat>();

        // Yoga specialties offered in this class
        public List<YogaSpeciality> Specialities { get; set; }
            = new List<YogaSpeciality>();
    }
}
