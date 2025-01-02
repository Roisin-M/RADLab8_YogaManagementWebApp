using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YogaManagement_ClassLibrary;
using YogaManagement_WebApp.Data;

namespace YogaManagement_WebApp.Pages.InstructorsClasses
{
    public class EditModel : PageModel
    {
        private readonly YogaManagement_WebApp.Data.ApplicationDbContext _context;

        public EditModel(YogaManagement_WebApp.Data.ApplicationDbContext context)
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

            var instructorclass =  await _context.InstructorClasses.FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructorclass == null)
            {
                return NotFound();
            }
            InstructorClass = instructorclass;
           ViewData["ClassId"] = new SelectList(_context.Classs, "Id", "Description");
           ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(InstructorClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorClassExists(InstructorClass.InstructorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InstructorClassExists(string id)
        {
            return _context.InstructorClasses.Any(e => e.InstructorId == id);
        }
    }
}
