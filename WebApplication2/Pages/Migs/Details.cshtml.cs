using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages.Migs
{
    public class DetailsModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public DetailsModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public Mig Mig { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mig = await _context.Migs
                .Include(m => m.AirplaneNavigation)
                .SingleOrDefaultAsync(m => m.SideNumber == id);

            if (Mig == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
