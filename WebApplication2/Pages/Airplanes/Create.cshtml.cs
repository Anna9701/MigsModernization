using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages.Airplanes
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
            var types = new List<String> { "Myśliwiec", "Szkolno-Bojowy" };
            ViewData["Type"] = new SelectList(types);
            return Page();
        }

        [BindProperty]
        public Airplane Airplane { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var a in _context.Airplanes.ToList())
            {
                if (a.Name.Equals(Airplane.Name) && a.Version.Equals(Airplane.Version))
                {
                    ModelState.AddModelError("Airplane.Name", "There is such airplane registered!");
                    ModelState.AddModelError("Airplane.Version", "There is such airplane registered!");
                }
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Airplanes.Add(Airplane);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}