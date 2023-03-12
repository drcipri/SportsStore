namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext _context;
        public IQueryable<Product> Products => _context.Products;
        public EFStoreRepository(StoreDbContext context)
        {
            _context = context;
        }

        public void SaveProduct(Product p)
        {
            _context.SaveChanges();
        }

        public void CreateProduct(Product p)
        {
            _context.Add(p);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            _context.Remove(p);
            _context.SaveChanges();
        }
    }
}
