using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;

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
