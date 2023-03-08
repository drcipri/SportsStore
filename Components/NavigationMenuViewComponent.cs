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
            ViewBag.SelectedCategory = RouteData?.Values["category"];//"category" => if the url had => http:.../Chess/Page1 => give me that category, http:.../Page1 => there is no category in the url.
            return View(_storeRepository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
