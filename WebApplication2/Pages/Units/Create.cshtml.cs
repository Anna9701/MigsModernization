using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MigsModernization.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MigsModernization.Pages.Units
{
    public class CreateModel : PageModel
    {
        private readonly MigsModernization.Context.Sin79_MigsModernizationContext _context;

        public CreateModel(MigsModernization.Context.Sin79_MigsModernizationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Unit Unit { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var unit in _context.Units.ToList())
            {
                if (unit.Name.Equals(Unit.Name))
                {
                    ModelState.AddModelError("Unit.Name", "There is such unit registered already.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Units.Add(Unit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}