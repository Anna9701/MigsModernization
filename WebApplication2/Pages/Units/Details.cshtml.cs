using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages.Units
{
    public class DetailsModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public DetailsModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public Unit Unit { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Unit = await _context.Units.SingleOrDefaultAsync(m => m.Id == id);

            if (Unit == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
