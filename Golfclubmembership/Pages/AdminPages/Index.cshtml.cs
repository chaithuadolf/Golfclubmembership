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
    public class IndexModel : PageModel
    {
        private readonly Golfclubmembership.Models.ApplicationDbContext _context;

        public IndexModel(Golfclubmembership.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ClubCards> ClubCards { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ClubCardsList != null)
            {
                ClubCards = await _context.ClubCardsList.ToListAsync();
            }
        }
    }
}
