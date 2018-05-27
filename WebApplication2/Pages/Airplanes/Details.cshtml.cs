﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Models;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Airplanes
{
    public class DetailsModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public DetailsModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

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
    }
}
