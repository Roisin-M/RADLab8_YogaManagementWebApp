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
    public class IndexModel : PageModel
    {
        private readonly YogaManagement_WebApp.Data.ApplicationDbContext _context;

        public IndexModel(YogaManagement_WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<InstructorClass> InstructorClass { get;set; } = default!;

        public async Task OnGetAsync()
        {
            InstructorClass = await _context.InstructorClasses
                .Include(i => i.FK_Class)
                .Include(i => i.FK_Instructor).ToListAsync();
        }
    }
}
