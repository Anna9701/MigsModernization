using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages.Migs.Modernizations
{
    public class EditModel : PageModel
    {
        private readonly Sin79_MigsModernizationContext _context;

        public EditModel(Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        private static string SideNumberParameterName => "sideNumber";

        [BindProperty]
        public Modernization Modernization { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Modernization = await _context.Modernization
                .Include(m => m.MigSideNumberNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (Modernization == null)
            {
                return NotFound();
            }

            InitializeModernizationTypesSelectList();
            return Page();
        }

        private void InitializeModernizationTypesSelectList()
        {
            List<String> modernizationTypes = new List<string>();
            var modernizations = _context.ModernizationTypes.ToList().Distinct();
            foreach (var type in modernizations)
            {
                modernizationTypes.Add(type.Name);
            }

            ViewData["ModernizationName"] = new SelectList(modernizationTypes);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CheckIfExistSuchModernization();
            CheckIfPerformed();
            if (!ModelState.IsValid)
            {
                return await OnGetAsync(Modernization.Id);
            }

            var modernization = await _context.Modernization.FirstOrDefaultAsync(m => m.Id == Modernization.Id);
            modernization.ModernizationName = Modernization.ModernizationName;
            modernization.Performed = Modernization.Performed;
            modernization.PlannedBy = Modernization.PlannedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModernizationExists(Modernization.Id))
                {
                    return NotFound();
                }
                else
                {
                    return await OnGetAsync(Modernization.Id);
                }
            }

            var routes = new RouteValueDictionary
            {
                { SideNumberParameterName, Modernization.MigSideNumber }
            };

            return RedirectToPage("./Index", routes);
        }

        private bool ModernizationExists(long id)
        {
            return _context.Modernization.Any(e => e.Id == id);
        }

        private void CheckIfPerformed()
        {
            if (!Modernization.Performed)
            {
                if (Modernization.PlannedBy == null)
                {
                    ModelState.AddModelError("Modernization.PlannedBy", "Planned modernization time is required");
                }
            }
            else
            {
                Modernization.PlannedBy = null;
            }
        }

        private void CheckIfExistSuchModernization()
        {
            var modernizations = _context.Modernization
                .Where(m => m.MigSideNumber == Modernization.MigSideNumber)
                .ToList();
            foreach (var modernization in modernizations)
            {
                if (modernization.ModernizationName.Equals(Modernization.ModernizationName) 
                    && modernization.Id != Modernization.Id)
                {
                    ModelState.AddModelError("Modernization.ModernizationName",
                        "Such modernization was registred already");
                }
            }
        }
    }
}
