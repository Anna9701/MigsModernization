using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages.StagingAreas
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
        public StagingArea StagingArea { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var area in _context.StagingAreas.ToList())
            {
                if (area.CityName.Equals(StagingArea.CityName))
                {
                    ModelState.AddModelError("StagingArea.CityName", "There is such staging area defined already.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StagingAreas.Add(StagingArea);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}