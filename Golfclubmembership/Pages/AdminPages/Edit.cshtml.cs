using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Golfclubmembership.Models;

namespace Golfclubmembership.Pages.AdminPages
{
    public class EditModel : PageModel
    {
        private readonly Golfclubmembership.Models.ApplicationDbContext _context;

        public EditModel(Golfclubmembership.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClubCards ClubCards { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ClubCardsList == null)
            {
                return NotFound();
            }

            var clubCards =  await _context.ClubCardsList.FirstOrDefaultAsync(m => m.Id == id);
            if (clubCards == null)
            {
                return NotFound();
            }
            ClubCards = clubCards;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ClubCards).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubCardsExists(ClubCards.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClubCardsExists(int id)
        {
          return (_context.ClubCardsList?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
