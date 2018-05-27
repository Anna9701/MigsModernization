using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Migs
{
    public class EditModel : PageModel
    {
        private readonly Sin79_MigsModernizationContext _context;

        [BindProperty]
        public Mig Mig { get; set; }

        public EditModel(Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mig = await _context.Migs
                .Include(m => m.Modernizations)
                .SingleOrDefaultAsync(m => m.SideNumber == id);

            if (Mig == null)
            {
                return NotFound();
            }

            InitializeStagingAreasSelectList();
            InitializeUnitsSelectList();
            InitializeAirplanesSelectList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await OnGetAsync(Mig.SideNumber);
            }

            try
            {
                await UpdateMig();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MigExists(Mig.SideNumber))
                {
                    return NotFound();
                }
                else
                {
                    return await OnGetAsync(Mig.SideNumber);
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task UpdateMig()
        {
            var migInDatabase = await _context.Migs
                .Include(m => m.Modernizations)
                .FirstOrDefaultAsync(m => m.SideNumber == Mig.SideNumber);

            migInDatabase.AirplaneId = Mig.AirplaneId;
            migInDatabase.Notes = Mig.Notes;
            migInDatabase.StagingArea = Mig.StagingArea;

            await _context.SaveChangesAsync();
        }

        //TODO fix editing!!

        private bool MigExists(long id)
        {
            return _context.Migs.Any(e => e.SideNumber == id);
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
    }
}
