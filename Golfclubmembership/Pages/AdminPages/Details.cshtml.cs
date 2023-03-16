using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Golfclubmembership.Models;

namespace Golfclubmembership.Pages.AdminPages
{
    public class DetailsModel : PageModel
    {
        private readonly Golfclubmembership.Models.ApplicationDbContext _context;

        public DetailsModel(Golfclubmembership.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public ClubCards ClubCards { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ClubCardsList == null)
            {
                return NotFound();
            }

            var clubCards = await _context.ClubCardsList.FirstOrDefaultAsync(m => m.Id == id);
            if (clubCards == null)
            {
                return NotFound();
            }
            else 
            {
                ClubCards = clubCards;
            }
            return Page();
        }
    }
}
