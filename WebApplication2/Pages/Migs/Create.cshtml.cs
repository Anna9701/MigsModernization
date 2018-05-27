using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MigsModernization.Context;
using MigsModernization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Migs
{
    public class CreateModel : PageModel
    {
        private readonly Sin79_MigsModernizationContext _context;

        public CreateModel(Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            InitializeStagingAreasSelectList();
            InitializeUnitsSelectList();
            InitializeAirplanesSelectList();

            return Page();
        }

        private void InitializeAirplanesSelectList()
        {
            List<Airplane> airplanesSelectListItems = _context.Airplanes.ToList();
            List<Airplane> copyList = new List<Airplane>();
            for (int i = 0; i < airplanesSelectListItems.Count; ++i)
            {
                Airplane airplane = airplanesSelectListItems.ElementAt(i);
                copyList.Add(new Airplane { Name = airplane.ToString(), Id = airplane.Id });
            }
            ViewData["AirplaneId"] = new SelectList(copyList, "Id", "Name");
        }

        private void InitializeStagingAreasSelectList()
        {
            List<String> stagingAreas = new List<string>();
            var areas = _context.StagingAreas.ToList().Distinct();
            foreach (var area in areas)
            {
                stagingAreas.Add(area.CityName);
            }
            ViewData["StagingArea"] = new SelectList(stagingAreas);
        }

        private void InitializeUnitsSelectList()
        {
            List<String> units = new List<string>();
            var unitsEntities = _context.Units.ToList().Distinct();
            foreach (var unit in unitsEntities)
            {
                units.Add(unit.Name);
            }
            ViewData["Unit"] = new SelectList(units);
        }

        [BindProperty]
        public Mig Mig { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            CheckIfSideNumberUnique();
            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            _context.Migs.Add(Mig);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void CheckIfSideNumberUnique()
        {
            if (_context.Migs.FirstOrDefault(m => m.SideNumber == Mig.SideNumber) != null)
            {
                ModelState.AddModelError("Mig.SideNumber", "There is mig with such side number registered!");
            }
        }
    }
}