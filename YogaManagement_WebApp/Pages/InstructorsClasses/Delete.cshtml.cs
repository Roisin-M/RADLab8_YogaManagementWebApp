using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YogaManagement_ClassLibrary;
using YogaManagement_WebApp.Data;

namespace YogaManagement_WebApp.Pages.InstructorsClasses
{
    public class DeleteModel : PageModel
    {
        private readonly YogaManagement_WebApp.Data.ApplicationDbContext _context;

        public DeleteModel(YogaManagement_WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstructorClass InstructorClass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructorclass = await _context.InstructorClasses.FirstOrDefaultAsync(m => m.InstructorId == id);

            if (instructorclass is not null)
            {
                InstructorClass = instructorclass;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructorclass = await _context.InstructorClasses.FindAsync(id);
            if (instructorclass != null)
            {
                InstructorClass = instructorclass;
                _context.InstructorClasses.Remove(InstructorClass);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
