using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Models;
using System;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Airplanes
{
    public class DeleteModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public DeleteModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Airplane Airplane { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Airplane = await _context.Airplanes.SingleOrDefaultAsync(m => m.Id == id);

            if (Airplane == null)
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

            Airplane = await _context.Airplanes.FindAsync(id);
            await CheckIfThereAreRelatedMigs();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Airplane != null)
            {
                _context.Airplanes.Remove(Airplane);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private async Task CheckIfThereAreRelatedMigs()
        {
            var migs = await _context.Migs.ToListAsync();
            foreach (var mig in migs)
            {
                if (mig.AirplaneId == Airplane.Id)
                {
                    ModelState.AddModelError("Airplane", "There are migs in that type registered! You cannot remove it.");
                }
            }

        }
    }
}
