using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages.Migs
{
    public class CreateModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public CreateModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Mig Mig { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Migs.Add(Mig);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}