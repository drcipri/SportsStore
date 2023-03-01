using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private IStoreRepository _storeRepository;
        public NavigationMenuViewComponent(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];//"category" is the routing URL app.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
            return View(_storeRepository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
