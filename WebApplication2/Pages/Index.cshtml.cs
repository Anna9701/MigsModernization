using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MigsModernization.Context;
using MigsModernization.Models;

namespace MigsModernization.Pages
{
    public class IndexModel : PageModel
    {
        private Sin79_MigsModernizationContext migsModernizationContext;
        private readonly DbSet<Mig> migs;

        public IndexModel()
        {
            migsModernizationContext = new Sin79_MigsModernizationContext();
            migsModernizationContext.Database.EnsureCreated();
            migs = migsModernizationContext.Migs;
        }

        public void OnGet()
        {

        }
    }
}
