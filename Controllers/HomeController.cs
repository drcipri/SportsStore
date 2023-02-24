using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _storeRepository;
        public HomeController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public IActionResult Index()
        {
            return View(_storeRepository.Products);
        }
    }
}
