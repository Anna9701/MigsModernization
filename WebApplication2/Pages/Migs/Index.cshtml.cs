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

namespace MigsModernization.Pages.Migs
{
    public class IndexModel : PageModel
    {
        private static String SideNumberParameterName => "sideNumber";
        public static String MigsModernizationsPage => "./Modernizations/Index";

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
            Migs = await _context.Migs.ToListAsync();
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

            return RedirectToPage(MigsModernizationsPage, routes);
        }
    }
}
