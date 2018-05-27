using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages.StagingAreas
{
    public class DeleteModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public DeleteModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StagingArea StagingArea { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StagingArea = await _context.StagingAreas.SingleOrDefaultAsync(m => m.Id == id);

            if (StagingArea == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StagingArea = await _context.StagingAreas.FindAsync(id);

            if (StagingArea != null)
            {
                _context.StagingAreas.Remove(StagingArea);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
