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

namespace MigsModernization.Pages.Units
{
    public class EditModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public EditModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var u in _context.Units.ToList())
            {
                if (u.Name.Equals(Unit.Name) && Unit.Id != u.Id)
                {
                    ModelState.AddModelError("Unit.Name", "There is such staging area defined already.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var unit = await _context.Units.FirstOrDefaultAsync(u => u.Id == Unit.Id);
            unit.Name = Unit.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(Unit.Id))
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

        private bool UnitExists(long id)
        {
            return _context.Units.Any(e => e.Id == id);
        }
    }
}
