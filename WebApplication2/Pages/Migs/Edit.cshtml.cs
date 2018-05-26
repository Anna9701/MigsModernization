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

namespace MigsModernization.Pages.Migs
{
    public class EditModel : PageModel
    {
        private readonly Sin79_MigsModernizationContext _context;

        public EditModel(Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mig Mig { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mig = await _context.Migs
                .Include(m => m.Modernizations)
                .SingleOrDefaultAsync(m => m.SideNumber == id);

            if (Mig == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await UpdateMig();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MigExists(Mig.SideNumber))
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

        private async Task UpdateMig()
        {
            var migInDatabes = await _context.Migs
                .Include(m => m.Modernizations)
                .FirstOrDefaultAsync(m => m.SideNumber == Mig.SideNumber);
            migInDatabes.Airplane = Mig.Airplane;
            migInDatabes.Notes = Mig.Notes;
            migInDatabes.StagingArea = Mig.StagingArea;
            migInDatabes.Unit = Mig.Unit;
            migInDatabes.Type = Mig.Type;
            migInDatabes.Version = Mig.Version;
            await _context.SaveChangesAsync();
        }

        private bool MigExists(long id)
        {
            return _context.Migs.Any(e => e.SideNumber == id);
        }
    }
}
