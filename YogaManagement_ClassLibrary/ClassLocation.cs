using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaManagement_ClassLibrary
{
    public class ClassLocation
    {
        public int Id { get; set; }
        public string description { get; set; }
        public string Location { get; set; }
        public int MaxCapacity { get; set; }
        //List of possible enum item class format
        public List<ClassFormat> FormatsAvailable { get; set; }
        //many - to - one relationship with class
        // Navigation property for related Classes
        public virtual ICollection<Class> Classes { get; set; }
            = new List<Class>();

    }
    public enum ClassFormat
    {
        Stream,
        OnLocation,
        Hybrid
    }
}
