using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Golfclubmembership.Models;

namespace Golfclubmembership.Pages
{
    public class ContactusModel : PageModel
    {
        public void OnGet()
        {
            List<ClubCardInCart> clubCardsInCart = new List<ClubCardInCart>();
            clubCardsInCart = SessionObjectHandler.GetObjectFromJsonString<List<ClubCardInCart>>(HttpContext.Session, "shoppingCardbag");
            if (clubCardsInCart != null)
                ViewData["CartItemCount"] = clubCardsInCart.Sum(item => item.Quantity);
        }
    }
}
