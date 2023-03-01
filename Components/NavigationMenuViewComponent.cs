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
            return View(_storeRepository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
