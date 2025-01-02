using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
namespace YogaManagement_ClassLibrary
{
    public class Instructor : IdentityUser
    {
       // public int Id { get; set; }
        public string Name { get; set; }
        //public string Email { get; set; }
        //List of enum values for Yoga Speciality
        public List<YogaSpeciality> Specialities { get; set; }
        //relationship With Class - many to many
        public virtual ICollection<InstructorClass> InstructorClasses { get; set; }
            = new List<InstructorClass>();
    }
    public enum YogaSpeciality
    {
        Hatha,
        Vinyasa,
        Ashtanga,
        Bikram,
        Iyengar,
        Kundalini,
        Yin,
        Restorative,
        PowerYoga,
        Jivamukti,
        Anusara,
        Sivananda,
        Prenatal,
        AerialYoga,
        AcroYoga,
        ChairYoga,
        Viniyoga,
        YogaNidra,
        IntegralYoga,
        TantraYoga
    }
}
