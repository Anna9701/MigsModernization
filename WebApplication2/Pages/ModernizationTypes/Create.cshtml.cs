using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MigsModernization.Models;
using System;
using System.Linq;

namespace MigsModernization.Pages.ModernizationTypes
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
        public ModernizationType ModernizationType { get; set; }

        public IActionResult OnPost()
        {
            foreach(var type in _context.ModernizationTypes.ToList())
            {
                if (type.Name.Equals(ModernizationType.Name))
                {
                    ModelState.AddModelError("ModernizationType.Name", "There is such type already.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ModernizationTypes.Add(ModernizationType);

            try
            {
                _context.SaveChanges();
            } catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
            }
    }
}