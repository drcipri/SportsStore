namespace SportsStore.Models
{
    public class EfOrderRepository: IOrderRepository
    {
        private StoreDbContext _context;
        public EfOrderRepository(StoreDbContext context)
        { 
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders.Include(o => o.CartLines).ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.CartLines.Select(l => l.Product));
            if(order.OrderId == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
    }
}
