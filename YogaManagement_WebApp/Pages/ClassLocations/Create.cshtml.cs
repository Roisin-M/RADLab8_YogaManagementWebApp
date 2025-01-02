using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using YogaManagement_ClassLibrary;
using YogaManagement_WebApp.Data;

namespace YogaManagement_WebApp.Pages.ClassLocations
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
            return Page();
        }

        [BindProperty]
        public ClassLocation ClassLocation { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ClassLocations.Add(ClassLocation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
