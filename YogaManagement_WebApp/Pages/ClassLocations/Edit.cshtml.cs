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

namespace YogaManagement_WebApp.Pages.ClassLocations
{
    public class EditModel : PageModel
    {
        private readonly YogaManagement_WebApp.Data.ApplicationDbContext _context;

        public EditModel(YogaManagement_WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClassLocation ClassLocation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classlocation =  await _context.ClassLocations.FirstOrDefaultAsync(m => m.Id == id);
            if (classlocation == null)
            {
                return NotFound();
            }
            ClassLocation = classlocation;
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

            _context.Attach(ClassLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassLocationExists(ClassLocation.Id))
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

        private bool ClassLocationExists(int id)
        {
            return _context.ClassLocations.Any(e => e.Id == id);
        }
    }
}
