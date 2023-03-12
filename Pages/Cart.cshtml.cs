using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;

namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepository _storeRepository;
      
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public CartModel(IStoreRepository storeRepository, Cart cartService)
        {
            _storeRepository = storeRepository;
            Cart = cartService;
        }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product? product = _storeRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            if(product != null)
            {
                Cart.AddItem(product, 1);
            }
            return RedirectToPage(new {returnUrl = returnUrl});
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductId == productId).Product);
            return RedirectToPage(new {returnUrl = returnUrl});
        } 
    }
}
