using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;

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

            if (Airplane != null)
            {
                _context.Airplanes.Remove(Airplane);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
