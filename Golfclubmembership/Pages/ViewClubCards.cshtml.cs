using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Golfclubmembership.Models;

namespace Golfclubmembership.Pages
{
    public class ViewClubCardsModel : PageModel
    {

        public readonly ApplicationDbContext _db;

        public ViewClubCardsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty(SupportsGet = true)]
        public ClubCards ClubCards { get; set; }
        public void OnGet(int id)
        {
            if (id != 0)
            {
                ClubCards = _db.ClubCardsList.Where(d => d.Id == id).FirstOrDefault();
            }
            else
            {

            }
            List<ClubCardInCart> clubCardsInCart = new List<ClubCardInCart>();
            clubCardsInCart = SessionObjectHandler.GetObjectFromJsonString<List<ClubCardInCart>>(HttpContext.Session, "shoppingCardbag");
            if (clubCardsInCart != null)
                ViewData["CartItemCount"] = clubCardsInCart.Sum(item => item.Quantity);
        }
        public IActionResult OnPost(int id)
        {
            return RedirectToPage("ShoppingCart", new { id = id });
        }

    }
}
