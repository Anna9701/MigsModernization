using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages.Migs.Modernizations
{
    public class DeleteModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public DeleteModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
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
                .Include(m => m.MigSideNumberNavigation.AirplaneNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (Modernization == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Modernization = await _context.Modernization.FindAsync(id);

            var routes = new RouteValueDictionary
            {
                { SideNumberParameterName, Modernization.MigSideNumber }
            };

            if (Modernization != null)
            {
                _context.Modernization.Remove(Modernization);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", routes);
        }
    }
}
