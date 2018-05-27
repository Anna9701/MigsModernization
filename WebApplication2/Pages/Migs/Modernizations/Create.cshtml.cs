using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using MigsModernization.Context;
using MigsModernization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Migs.Modernizations
{
    public class CreateModel : PageModel
    {
        private readonly Sin79_MigsModernizationContext _context;

        public CreateModel(Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(long? migSideNumber)
        {
            if (!migSideNumber.HasValue ||
                _context.Migs.FirstOrDefault(m => m.SideNumber == migSideNumber) == null)
            {
                return NotFound();
            }

            Modernization = new Modernization
            {
                MigSideNumber = migSideNumber.Value
            };

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

        [BindProperty]
        public Modernization Modernization { get; set; }

        private static string SideNumberParameterName => "sideNumber";

        public async Task<IActionResult> OnPostAsync()
        {
            CheckIfExistSuchModernization();
            CheckIfPerformed();

            if (!ModelState.IsValid)
            {
                return OnGet(Modernization.MigSideNumber);
            }

            _context.Modernization.Add(Modernization);
            await _context.SaveChangesAsync();

            var routes = new RouteValueDictionary
            {
                { SideNumberParameterName, Modernization.MigSideNumber }
            };

            return RedirectToPage("./Index", routes);
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
                if (modernization.ModernizationName.Equals(Modernization.ModernizationName))
                {
                    ModelState.AddModelError("Modernization.ModernizationName", 
                        "Such modernization was registred already");
                }
            }
        }
    }
}