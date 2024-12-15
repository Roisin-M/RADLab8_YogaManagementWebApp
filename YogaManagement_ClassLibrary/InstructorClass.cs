using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaManagement_ClassLibrary
{
    //junction table for the many-to-many relationship between Instructor and class
    public class InstructorClass
    {
        public int InstructorId { get; set; }
        public int ClassId { get; set; }
        public virtual Instructor FK_Instructor { get; set; }
        public virtual Class FK_Class { get; set; }
    }
}
