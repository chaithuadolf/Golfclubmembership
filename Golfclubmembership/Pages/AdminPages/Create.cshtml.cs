using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Golfclubmembership.Models;

namespace Golfclubmembership.Pages.AdminPages
{
    public class CreateModel : PageModel
    {
        private readonly Golfclubmembership.Models.ApplicationDbContext _context;

        public CreateModel(Golfclubmembership.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ClubCards ClubCards { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ClubCardsList == null || ClubCards == null)
            {
                return Page();
            }

            _context.ClubCardsList.Add(ClubCards);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
