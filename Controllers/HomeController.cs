using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _storeRepository;
        public int PageSize = 4;
        public HomeController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public IActionResult Index(string? category, int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = _storeRepository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductId).Skip((productPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = productPage, 
                                              ItemsPerPage = PageSize, 
                                              TotalItems = category == null ? _storeRepository.Products.Count() : _storeRepository.Products.Where(x => x.Category == category).Count() },
                CurrentCategory = category, 
            });
        }
    }
}
