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

namespace MigsModernization.Pages.StagingAreas
{
    public class EditModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public EditModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var s in _context.StagingAreas.ToList())
            {
                if (s.CityName.Equals(StagingArea.CityName) && StagingArea.Id != s.Id)
                {
                    ModelState.AddModelError("StagingArea.CityName", "There is such staging area defined already.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var stagingArea = await _context.StagingAreas.FirstOrDefaultAsync(s => s.Id == StagingArea.Id);
            stagingArea.CityName = StagingArea.CityName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StagingAreaExists(StagingArea.Id))
                {
                    return NotFound();
                }
                else
                {
                    return Page(); 
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StagingAreaExists(long id)
        {
            return _context.StagingAreas.Any(e => e.Id == id);
        }
    }
}
