using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Migs.Modernizations
{
    public class IndexModel : PageModel
    {
        private readonly Sin79_MigsModernizationContext _context;

        [BindProperty]
        public long SideNumber { get; set; }

        public IndexModel(Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public IList<Modernization> Modernization { get;set; }

        public async Task OnGetAsync(long? sideNumber)
        {
            if (sideNumber.HasValue)
                SideNumber = sideNumber.Value;

            Modernization = await _context.Modernization
                .Include(m => m.MigSideNumberNavigation)
                .Where(m => m.MigSideNumber == sideNumber)
                .ToListAsync();
        }
    }
}
