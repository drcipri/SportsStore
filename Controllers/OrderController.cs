using Microsoft.AspNetCore.Mvc;
namespace SportsStore.Controllers
{
    public class OrderController: Controller
    {
        private IOrderRepository _repository;
        private Cart _cart;
        public OrderController(IOrderRepository orderRepository, Cart cartService)
        {
            _repository= orderRepository;
            _cart= cartService;
        }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if(_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if(ModelState.IsValid)
            {
                order.CartLines = _cart.Lines.ToArray();
                _repository.SaveOrder(order);  
                _cart.Clear();
                return RedirectToPage("/Completed", new {orderId = order.OrderId});
            }
            else
            {
                return View();
            }
        }
    }
}
