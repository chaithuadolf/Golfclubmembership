using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Golfclubmembership.Models;
using Golfclubmembership;

namespace Golfclubmembership.Pages
{
    public class ShoppingCartModel : PageModel
    {
        [BindProperty]
        public List<ClubCardInCart> cartClubCards { get; set; }

        public int FinalCartPrice { get; set; }

        public readonly ApplicationDbContext _db;

        public ShoppingCartModel(ApplicationDbContext db)
        {
            _db = db;

        }

        public string DeliveryAddress { get; set; }
        public Order order { get; set; }

        [BindProperty(SupportsGet =true)]
        public int id { get; set; }
        public void OnGet(int id)
        {
            cartClubCards = new List<ClubCardInCart>();
            cartClubCards = SessionObjectHandler.GetObjectFromJsonString<List<ClubCardInCart>>(HttpContext.Session, "shoppingCardbag");
            if (id != 0)
            {


                if (cartClubCards == null)

                    cartClubCards = new List<ClubCardInCart>();



                int clubCardItemIndex = cartClubCards.FindIndex(d => d.ClubCards.Id == id);
                if (clubCardItemIndex == -1)
                {
                    cartClubCards.Add(new ClubCardInCart
                    {
                        ClubCards = _db.ClubCardsList.Where(d => d.Id == id).FirstOrDefault(),
                        Quantity = 1,
                        TotalClubCardPrice = _db.ClubCardsList.Where(d => d.Id == id).FirstOrDefault().Price
                    }); ;
                }
                else
                {
                    cartClubCards[clubCardItemIndex].Quantity = cartClubCards[clubCardItemIndex].Quantity + 1;
                    cartClubCards[clubCardItemIndex].TotalClubCardPrice = cartClubCards[clubCardItemIndex].Quantity * cartClubCards[clubCardItemIndex].ClubCards.Price;
                }

                SessionObjectHandler.SetObjectAsJsonString(HttpContext.Session, "shoppingCardbag", cartClubCards);
            }
                if (cartClubCards != null)
                {
                    FinalCartPrice = cartClubCards.Sum(item => item.TotalClubCardPrice);
                    ViewData["CartItemCount"] = cartClubCards.Sum(item => item.Quantity);
                }
        }


        public IActionResult OnPost(int id,string method)
        {
            cartClubCards = new List<ClubCardInCart>();
            cartClubCards = SessionObjectHandler.GetObjectFromJsonString<List<ClubCardInCart>>(HttpContext.Session, "shoppingCardbag");

           
                
                if (method.Equals("increase"))
                {
                int clubCardsIndex = cartClubCards.FindIndex(x => x.ClubCards.Id == id);
                cartClubCards[clubCardsIndex].Quantity += 1;
                cartClubCards[clubCardsIndex].TotalClubCardPrice = (int)(cartClubCards[clubCardsIndex].Quantity * cartClubCards[clubCardsIndex].ClubCards.Price);
                }
                else if (method.Equals("decrease"))
                {
                int clubCardsIndex = cartClubCards.FindIndex(x => x.ClubCards.Id == id);
                if (cartClubCards[clubCardsIndex].Quantity == 1)
                    {
                        cartClubCards.RemoveAt(clubCardsIndex);
                    }
                    else
                    {
                        cartClubCards[clubCardsIndex].Quantity -= 1;
                    cartClubCards[clubCardsIndex].TotalClubCardPrice = (int)(cartClubCards[clubCardsIndex].Quantity * cartClubCards[clubCardsIndex].ClubCards.Price);
                    }
                }
                else if (method.Equals("delete"))
                {
                cartClubCards.RemoveAll(x => x.ClubCards.Id == id);
                }
                else if (method.Equals("placeorder"))
                {
                    var userId = HttpContext.User.Identity.Name;
                    foreach (var Item in cartClubCards)
                    {
                        order = new Order
                        {
                            UserId = userId,
                            Address = DeliveryAddress,
                            Quantity = Item.Quantity,
                            pid = Item.ClubCards.Id,
                        };

                        try
                        {
                            _db.Order.Add(order);
                            _db.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                cartClubCards.Clear();
                return RedirectToPage("Checkout");
            }

                SessionObjectHandler.SetObjectAsJsonString(HttpContext.Session, "shoppingCardbag", cartClubCards);

                if (cartClubCards != null)
                {
                    FinalCartPrice = cartClubCards.Sum(item => item.TotalClubCardPrice);
                    ViewData["CartItemCount"] = cartClubCards.Sum(item => item.Quantity);
                }
            return Page();
            
        }
    }
}
