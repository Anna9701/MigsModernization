using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Airplanes
{
    public class EditModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public EditModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
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

            var types = new List<String> { "Myśliwiec", "Szkolno-Bojowy" };
            ViewData["Type"] = new SelectList(types);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var a in _context.Airplanes.ToList())
            {
                if (a.Name.Equals(Airplane.Name) && a.Version.Equals(Airplane.Version) && a.Id != Airplane.Id)
                {
                    ModelState.AddModelError("Airplane.Name", "There is such airplane registered!");
                    ModelState.AddModelError("Airplane.Version", "There is such airplane registered!");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var airplane = await _context.Airplanes.FirstOrDefaultAsync(a => a.Id == Airplane.Id);
            airplane.Name = Airplane.Name;
            airplane.Version = Airplane.Version;
            airplane.Type = Airplane.Type;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirplaneExists(Airplane.Id))
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

        private bool AirplaneExists(long id)
        {
            return _context.Airplanes.Any(e => e.Id == id);
        }
    }
}
