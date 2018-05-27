using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MigsModernization.Pages.ModernizationTypes
{
    public class IndexModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public IndexModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public IList<ModernizationType> ModernizationType { get;set; }

        public async Task OnGetAsync()
        {
            ModernizationType = await _context.ModernizationTypes.ToListAsync();
        }
    }
}
