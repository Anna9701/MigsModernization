using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Units
{
    public class IndexModel : PageModel
    {
        private readonly Sin79_MigsModernizationContext _context;

        public IndexModel(Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public IList<Unit> Unit { get;set; }

        public async Task OnGetAsync()
        {
            Unit = await _context.Units.ToListAsync();
        }
    }
}
