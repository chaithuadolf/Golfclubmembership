using Golfclubmembership.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Golfclubmembership.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            List<ClubCardInCart> clubCardInCart = new List<ClubCardInCart>();
            clubCardInCart = SessionObjectHandler.GetObjectFromJsonString<List<ClubCardInCart>>(HttpContext.Session, "shoppingCardbag");
            if (clubCardInCart != null)
                ViewData["CartItemCount"] = clubCardInCart.Sum(item => item.Quantity);
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("ViewClubCardsList");
        }
    }
}