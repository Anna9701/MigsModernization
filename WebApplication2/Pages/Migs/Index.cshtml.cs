using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Migs
{
    public class IndexModel : PageModel
    {
        private static String SideNumberParameterName => "id";
        public static String MigsDetailsPage => "./Details";

        [BindProperty]
        public long MigSideNumber { get; set; }

        public String InvalidMigSideNumberAlert => "There is no Mig with such Side Number";

        private readonly Sin79_MigsModernizationContext _context;

        public IndexModel(Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public IList<Mig> Migs { get; set; }

        public async Task OnGetAsync()
        {
            Migs = await _context.Migs
                .Include(m => m.AirplaneNavigation)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Mig mig = await _context.Migs.FindAsync(MigSideNumber);
            if (!ModelState.IsValid || mig == null)
            {
                await OnGetAsync();
                ModelState.AddModelError("MigSideNumber", InvalidMigSideNumberAlert);
                return Page();
            }

            var routes = new RouteValueDictionary
            {
                { SideNumberParameterName, MigSideNumber }
            };

            return RedirectToPage(MigsDetailsPage, routes);
        }
    }
}
