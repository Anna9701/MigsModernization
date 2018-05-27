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

namespace MigsModernization.Pages.ModernizationTypes
{
    public class EditModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public EditModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ModernizationType ModernizationType { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ModernizationType = await _context.ModernizationTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (ModernizationType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var t in _context.ModernizationTypes.ToList())
            {
                if (t.Name.Equals(ModernizationType.Name) && ModernizationType.Id != t.Id)
                {
                    ModelState.AddModelError("ModernizationType.Name", "There is such type already.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var type = _context.ModernizationTypes.FirstOrDefault(t => t.Id == ModernizationType.Id);
            type.Name = ModernizationType.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModernizationTypeExists(ModernizationType.Id))
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

        private bool ModernizationTypeExists(long id)
        {
            return _context.ModernizationTypes.Any(e => e.Id == id);
        }
    }
}
