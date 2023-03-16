using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Golfclubmembership.Models;

namespace Golfclubmembership.Pages
{
    public class ViewClubCardListModel : PageModel
    {
        public readonly ApplicationDbContext _db;

        public ViewClubCardListModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<ClubCards> ClubCards { get; set; }

        [BindProperty(SupportsGet =true)]
        public string Input { get; set; }
        public void OnGet()
        {
            ClubCards = _db.ClubCardsList.ToList();
            List<ClubCardInCart> clubCardInCart = new List<ClubCardInCart>();
            clubCardInCart = SessionObjectHandler.GetObjectFromJsonString<List<ClubCardInCart>>(HttpContext.Session, "shoppingCardbag");
            if (clubCardInCart != null)
                ViewData["CartItemCount"] = clubCardInCart.Sum(item => item.Quantity);
        }

        public void OnPost()
        {
            if (string.IsNullOrEmpty(Input))
            {

                ClubCards = _db.ClubCardsList.ToList();
            }
            else
            {
                ClubCards = _db.ClubCardsList.Where(item => item.Title.ToLower().Contains(Input) || item.Description.ToLower().Contains(Input)).ToList();
            }
        }
    }
}
