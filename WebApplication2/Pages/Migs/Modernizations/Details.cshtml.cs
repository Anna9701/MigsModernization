using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Models;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Migs.Modernizations
{
    public class DetailsModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public DetailsModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public Modernization Modernization { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Modernization = await _context.Modernization
                .Include(m => m.MigSideNumberNavigation).SingleOrDefaultAsync(m => m.Id == id);

            if (Modernization == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
