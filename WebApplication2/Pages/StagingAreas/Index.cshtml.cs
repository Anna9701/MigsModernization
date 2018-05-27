using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MigsModernization.Pages.StagingAreas
{
    public class IndexModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public IndexModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public IList<StagingArea> StagingArea { get;set; }

        public async Task OnGetAsync()
        {
            StagingArea = await _context.StagingAreas.ToListAsync();
        }
    }
}
