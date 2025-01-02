using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using YogaManagement_ClassLibrary;
using YogaManagement_WebApp.Data;

namespace YogaManagement_WebApp.Pages.InstructorsClasses
{
    public class CreateModel : PageModel
    {
        private readonly YogaManagement_WebApp.Data.ApplicationDbContext _context;

        public CreateModel(YogaManagement_WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClassId"] = new SelectList(_context.Classs, "Id", "Description");
        ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public InstructorClass InstructorClass { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InstructorClasses.Add(InstructorClass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
