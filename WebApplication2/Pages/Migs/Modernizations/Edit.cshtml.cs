using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages.Migs.Modernizations
{
    public class EditModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public EditModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Modernization Modernization { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Modernization = await _context.Modernization
                .Include(m => m.MigSideNumberNavigation).SingleOrDefaultAsync(m => m.Id == id);

            if (Modernization == null)
            {
                return NotFound();
            }
           ViewData["MigSideNumber"] = new SelectList(_context.Migs, "SideNumber", "Airplane");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Modernization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModernizationExists(Modernization.Id))
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

        private bool ModernizationExists(long id)
        {
            return _context.Modernization.Any(e => e.Id == id);
        }
    }
}
